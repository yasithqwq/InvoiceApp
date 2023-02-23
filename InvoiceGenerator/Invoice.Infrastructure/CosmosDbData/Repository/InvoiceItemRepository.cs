using Invoice.Domain.Entities;
using Invoice.Domain.Interfaces.Persistence;
using Invoice.Infrastructure.CosmosDbData.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.CosmosDbData.Repository
{
    public class InvoiceItemRepository : CosmosDbRepository<InvoiceItem>, IInvoiceItemRepository
    {
        /// <summary>
        ///     CosmosDB container name
        /// </summary>
        public override string ContainerName { get; } = "Invoices";

        /// <summary>
        ///     Generate Id.
        ///     e.g. "shoppinglist:783dfe25-7ece-4f0b-885e-c0ea72135942"
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string GenerateId(InvoiceItem entity) => $"{entity.Date}:{Guid.NewGuid()}";

        /// <summary>
        ///     Returns the value of the partition key
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);

        public InvoiceItemRepository(ICosmosDbContainerFactory factory, CosmosDbSeed seed) : base(factory)
        {}

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Category is also an example of single partition read, where get by title will be a cross partition read
        public async Task<IEnumerable<InvoiceItem>> GetItemsAsyncByCategory(string category)
        {
            List<InvoiceItem> results = new List<InvoiceItem>();
            string query = @$"SELECT c.Name FROM c WHERE c.Category = @Category";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@Category", category);
            string queryString = queryDefinition.QueryText;

            IEnumerable<InvoiceItem> entities = await this.GetItemsAsync(queryString);

            return results;
        }

        // Use Cosmos DB Parameterized Query to avoid SQL Injection.
        // Get by Title is also an example of cross partition read, where Get by Category will be single partition read
        public async Task<IEnumerable<InvoiceItem>> GetItemsAsyncByTitle(string title)
        {
            List<InvoiceItem> results = new List<InvoiceItem>();
            string query = @$"SELECT c.Name FROM c WHERE c.Title = @Title";

            QueryDefinition queryDefinition = new QueryDefinition(query)
                                                    .WithParameter("@Title", title);
            string queryString = queryDefinition.QueryText;

            IEnumerable<InvoiceItem> entities = await this.GetItemsAsync(queryString);

            return results;
        }
    }
}
