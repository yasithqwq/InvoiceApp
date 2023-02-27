

namespace Invoice.Application.IntegrationTests.InvoiceTests
{
    using FluentAssertions;
    using Invoice.Application.Invoices.Commands;
    using Invoice.Application.Invoices.Dtos;
    using Invoice.Domain.Entities;
    using System.Globalization;
    using static Testing;
    public class CreateInvoiceTest : TestBase
    {
        [Test]
        public async Task ShouldCreateInvoiceClass()
        {
            InvoiceItemDto invoice = new InvoiceItemDto()
            {
                Date = DateTime.Today.AddDays(-1).ToString("dd_MM_yyyy", CultureInfo.InvariantCulture),
                Description = $"Created by Unit Test. Invoice Number - {1}",
                InvoiceLines = new List<InvoiceLineDto> {
                            new InvoiceLineDto {Amount = Decimal.Multiply((decimal)6.2d, 1),Quantity=1+1,UnitPrice= Decimal.Multiply((decimal)2.2d, 1)},
                            new InvoiceLineDto {Amount = Decimal.Multiply((decimal)7.2d, 1),Quantity=1+1,UnitPrice= Decimal.Multiply((decimal)3.2d, 1)}
                        }
            };
            InvoiceItemDto response = await SendAsync(new CreateInvoiceCommand() { InvoiceItemDto = invoice });
            response.Id.Should().NotBeNullOrEmpty();
        }
    }
}
