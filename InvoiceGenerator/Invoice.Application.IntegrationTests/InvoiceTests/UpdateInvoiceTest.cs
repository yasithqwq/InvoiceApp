

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

            UpdateInvoiceItemDto invoiceDto = new  UpdateInvoiceItemDto()
            {
                Id= invoice.Id,
                Description = invoice.Description,
                TotalAmount = invoice.TotalAmount
            };


            UpdateInvoiceItemDto response = await SendAsync(new UpdateInvoiceCommand() { InvoiceItemDto = invoiceDto });
            response.Description.Should().BeEquivalentTo("Updated Invoice by Unit Test");
        }
    }
}
