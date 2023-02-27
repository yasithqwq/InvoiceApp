using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Dtos
{
    public class UpdatedResultDto
    {
        public bool IsSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public string UpdatedObjectId { get; set; }
        public UpdateInvoiceItemDto UpdatedObject { get; set; }
    }
}
