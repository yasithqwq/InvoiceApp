

namespace Invoice.Application.IntegrationTests.InvoiceTests
{
    using FluentAssertions;
    using Invoice.Application.Invoices.Commands;
    using Invoice.Application.Invoices.Dtos;
    using Invoice.Domain.Entities;
    using System.Globalization;

    using static Testing;
    public class UpdateInvoiceTest : TestBase
    {
        [Test]
        public async Task ShouldUpdateInvoiceClass()
        {
            var invoice = await FirstAsync<InvoiceItem>();
            invoice.Description = "Updated Invoice by Unit Test";

            InvoiceItemDto invoiceDto = new InvoiceItemDto()
            {
                Id= invoice.Id,
                Date = invoice.Date,
                Description = invoice.Description,
                TotalAmount = invoice.TotalAmount
            };


            InvoiceItemDto response = await SendAsync(new UpdateInvoiceCommand() { InvoiceItemDto = invoiceDto });
            response.Description.Should().BeEquivalentTo("Updated Invoice by Unit Test");
        }
    }
}
