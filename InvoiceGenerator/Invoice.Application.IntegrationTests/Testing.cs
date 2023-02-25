using Invoice.Domain.Entities;
using Invoice.Infrastructure;
using Invoice.Infrastructure.AppSettings;
using Invoice.Infrastructure.CosmosDbData;
using Invoice.Infrastructure.CosmosDbData.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Transactions;

namespace Invoice.Application.IntegrationTests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;

        public static IServiceScopeFactory ScopeFactory
        {
            get { return _scopeFactory; }
            set { _scopeFactory = value; }
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(_configuration);
            services.AddSingleton<ICosmosDbSeed, CosmosDbSeed>();
            services.AddApplication();
            services.AddInfrastructure(_configuration);
            services.AddHttpContextAccessor();
            services.AddLogging();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Bind database-related bindings
            CosmosDbSettings cosmosDbConfig = _configuration.GetSection("ConnectionStrings:CleanArchitectureCosmosDB").Get<CosmosDbSettings>();
            // register CosmosDB client and data repositories
            services.AddCosmosDb(cosmosDbConfig.EndpointUrl,
                                        cosmosDbConfig.PrimaryKey,
                                        cosmosDbConfig.DatabaseName,
                                        cosmosDbConfig.Containers);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var response = default(TResponse);
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<ISender>();

            using (TransactionScope tscope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                response = await mediator.Send(request);
                tscope.Dispose();
            }
            return response;
        }

        public static async Task<TEntity> FirstAsync<TEntity>()
       where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var _cosmosDbContainerFactory = scope.ServiceProvider.GetService<ICosmosDbContainerFactory>();
            var container = _cosmosDbContainerFactory.GetContainer("Invoices")._container;

            string queryString = "SELECT top 1 * FROM c";
            FeedIterator<TEntity> resultSetIterator = container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryString));
            List<TEntity> results = new List<TEntity>();
            while (resultSetIterator.HasMoreResults)
            {
                FeedResponse<TEntity> response = await resultSetIterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results.FirstOrDefault();
        }

        public static async Task<IList<TEntity>> TopAsync<TEntity>(int count)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var _cosmosDbContainerFactory = scope.ServiceProvider.GetService<ICosmosDbContainerFactory>();
            var container = _cosmosDbContainerFactory.GetContainer("Invoices")._container;

            string queryString = $"SELECT top {count} * FROM c";
            FeedIterator<TEntity> resultSetIterator = container.GetItemQueryIterator<TEntity>(new QueryDefinition(queryString));
            List<TEntity> results = new List<TEntity>();
            while (resultSetIterator.HasMoreResults)
            {
                FeedResponse<TEntity> response = await resultSetIterator.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }
    }
}