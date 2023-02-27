

namespace Invoice.Application.IntegrationTests.InvoiceTests
{
    using Ardalis.Specification;
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
            invoiceDto.InvoiceLines = new List<InvoiceLineDto>();
            decimal totalAmount = 0;
            foreach (InvoiceLine lineItem in invoice.InvoiceLines)
            {
                InvoiceLineDto itemLine = new InvoiceLineDto();
                itemLine.Quantity = lineItem.Quantity;
                itemLine.UnitPrice = lineItem.UnitPrice;
                itemLine.LineAmount = itemLine.Quantity * itemLine.UnitPrice;
                itemLine.Amount = itemLine.Quantity * itemLine.UnitPrice;
                invoiceDto.InvoiceLines.Add(itemLine);
                totalAmount += itemLine.Amount;
            }
            invoiceDto.TotalAmount = totalAmount;

            UpdateInvoiceItemDto response = await SendAsync(new UpdateInvoiceCommand() { InvoiceItemDto = invoiceDto });
            response.Description.Should().BeEquivalentTo("Updated Invoice by Unit Test");
        }
    }
}
