using AutoMapper;
using Invoice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Dtos
{
    public class InvoiceLineDtoToInvoiceLineMapper : Profile
    {
        public InvoiceLineDtoToInvoiceLineMapper()
        {
            CreateMap<InvoiceLineDto, InvoiceLine>();
        }
    }

    public class InvoiceLineToInvoiceLineDtoMapper : Profile
    {
        public InvoiceLineToInvoiceLineDtoMapper()
        {
            CreateMap<InvoiceLine, InvoiceLineDto>();
        }
    }
}
