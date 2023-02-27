using Invoice.Application.Invoices.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Commands
{
    public class UpdateInvoiceCommand : IRequest<UpdateInvoiceItemDto>
    {
        public UpdateInvoiceItemDto InvoiceItemDto { get; set; }
    }
}
