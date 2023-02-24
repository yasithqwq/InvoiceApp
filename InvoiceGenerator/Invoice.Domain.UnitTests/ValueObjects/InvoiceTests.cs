using Invoice.Domain.Entities;
using NUnit.Framework;
using System.Globalization;

namespace Invoice.Domain.UnitTests.ValueObjects
{
    public class Tests
    {
        [Test]
        public void shouldreturncorrectInvoice()
        {
            InvoiceItem invoice = new InvoiceItem {
                Date = DateTime.Today.AddDays(-1).ToString("dd_MM_yyyy", CultureInfo.InvariantCulture),
                Description = "Temp Description for Test"
            };
            Assert.AreEqual(invoice.Description, "Temp Description for Test");
        }
    }
}
