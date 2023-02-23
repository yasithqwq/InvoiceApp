using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Dtos
{
    public class InvoiceLineDto
    {
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineAmount { get; set; }
    }
}
