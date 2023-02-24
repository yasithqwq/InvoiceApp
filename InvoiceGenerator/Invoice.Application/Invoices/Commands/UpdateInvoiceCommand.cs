using Invoice.Application.Invoices.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Commands
{
    public class UpdateInvoiceCommand : IRequest<InvoiceItemDto>
    {
        public InvoiceItemDto InvoiceItemDto { get; set; }
    }
}
