using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Dtos
{
    public class UpdateInvoiceItemDto
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceLineDto> InvoiceLines { get; set; }
    }
}
