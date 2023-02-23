using Invoice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Interfaces.Persistence
{
    public interface IInvoiceItemRepository : IRepository<InvoiceItem>
    {
        Task<IEnumerable<InvoiceItem>> GetItemsAsyncByDescription(string description);
        Task<IEnumerable<InvoiceItem>> GetItemsAsyncByDate(string date);
    }
}
