using AutoMapper;
using Invoice.Application.Common.Mappings;
using Invoice.Application.Invoices.Dtos;
using Invoice.Domain.Entities;
using System.Runtime.Serialization;


namespace Invoice.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<InvoiceItemDtoToInvoiceItemMapper>();
                cfg.AddProfile<InvoiceItemToInvoiceItemDtoMapper>();
                cfg.AddProfile<InvoiceLineDtoToInvoiceLineMapper>();
                cfg.AddProfile<InvoiceLineToInvoiceLineDtoMapper>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        [TestCase(typeof(InvoiceItemDto), typeof(InvoiceItem))]
        [TestCase(typeof(InvoiceItem), typeof(InvoiceItemDto))]
        [TestCase(typeof(InvoiceLineDto), typeof(InvoiceLine))]
        [TestCase(typeof(InvoiceLine), typeof(InvoiceLineDto))]
        public void ShouldSupportMappingFromSourceToDestination_PropertySchema(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
