using AutoMapper;
using Invoice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Application.Invoices.Dtos
{
    public class UpdateInvoiceItemDtoToInvoiceItemMapper : Profile
    {
        public UpdateInvoiceItemDtoToInvoiceItemMapper()
        {
            CreateMap<UpdateInvoiceItemDto, InvoiceItem>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.InvoiceLines, opt => opt.MapFrom(s => s.InvoiceLines));
        }
    }

    public class InvoiceItemToUpdateInvoiceItemDtoMapper : Profile
    {
        public InvoiceItemToUpdateInvoiceItemDtoMapper()
        {
            CreateMap<InvoiceItem, UpdateInvoiceItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.InvoiceLines, opt => opt.MapFrom(s => s.InvoiceLines));
        }
    }
}
