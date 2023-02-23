using Invoice.Domain.Entities;
using AutoMapper;


namespace Invoice.Application.Invoices.Dtos
{
    public class InvoiceItemDtoToInvoiceItemMapper : Profile
    {
        public InvoiceItemDtoToInvoiceItemMapper()
        {
            CreateMap<InvoiceItemDto, InvoiceItem>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.InvoiceLines, opt => opt.MapFrom(s => s.InvoiceLines));
        }
    }

    public class InvoiceItemToInvoiceItemDtoMapper : Profile
    {
        public InvoiceItemToInvoiceItemDtoMapper()
        {
            CreateMap<InvoiceItem, InvoiceItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.InvoiceLines, opt => opt.MapFrom(s => s.InvoiceLines));
        }
    }
}
