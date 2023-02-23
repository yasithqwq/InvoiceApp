using Invoice.Domain.Entities;
using Invoice.Domain.Interfaces.Persistence;
using Invoice.Infrastructure.CosmosDbData.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.CosmosDbData
{
    public class CosmosDbSeed: ICosmosDbSeed
    {
        private readonly IInvoiceItemRepository _repo;

        public CosmosDbSeed(IInvoiceItemRepository repo)
        {
            this._repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task InvoiceItemSeed()
        {
            string sqlQueryText = "SELECT * FROM c";
            IEnumerable<InvoiceItem> invoices = await _repo.GetItemsAsync(sqlQueryText);

            if (invoices.Count() == 0)
            {
                for (int i = 1; i <= 5; i++)
                {
                    InvoiceItem invoice = new InvoiceItem()
                    {
                        Date = DateTime.Today.AddDays(-i).ToString("dd_MM_yyyy", CultureInfo.InvariantCulture),
                        Description = $"Random invoice created by seed. Invoice Number - {i}",
                        TotalAmount = Decimal.Multiply((decimal)4.2d, i),
                        InvoiceLines = new List<InvoiceLine> {
                            new InvoiceLine {Amount = Decimal.Multiply((decimal)6.2d, i),LineAmount = Decimal.Multiply((decimal)7.2d, i),Quantity=i+1,UnitPrice= Decimal.Multiply((decimal)2.2d, i)},
                            new InvoiceLine {Amount = Decimal.Multiply((decimal)7.2d, i),LineAmount = Decimal.Multiply((decimal)8.2d, i),Quantity=i+1,UnitPrice= Decimal.Multiply((decimal)3.2d, i)}
                        }
                    };

                    await _repo.AddItemAsync(invoice);
                }
            }
        }
    }
}
