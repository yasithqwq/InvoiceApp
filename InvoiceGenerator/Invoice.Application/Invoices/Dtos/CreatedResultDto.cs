using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Dtos
{
    public class CreatedResultDto
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public string CreatedObjectId { get; set; }
        public InvoiceItemDto CreatedObject { get; set; }
    }
}
