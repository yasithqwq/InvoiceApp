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
    }
}
