using Invoice.Application;
using Invoice.Domain.Interfaces.Persistence;
using Invoice.Infrastructure;
using Invoice.Infrastructure.AppSettings;
using Invoice.Infrastructure.CosmosDbData.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Invoice.Api.Startup))]
namespace Invoice.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //var configuration = builder.GetContext().Configuration;
            // Configurations
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Bind database-related bindings
            CosmosDbSettings cosmosDbConfig = configuration.GetSection("ConnectionStrings:CleanArchitectureCosmosDB").Get<CosmosDbSettings>();
            // register CosmosDB client and data repositories
            builder.Services.AddCosmosDb(cosmosDbConfig.EndpointUrl,
                                 cosmosDbConfig.PrimaryKey,
            cosmosDbConfig.DatabaseName,
                                 cosmosDbConfig.Containers);
            builder.Services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
        }
    }
}
