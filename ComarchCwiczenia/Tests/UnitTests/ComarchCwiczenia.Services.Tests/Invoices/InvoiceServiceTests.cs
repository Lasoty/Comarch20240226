using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Domain.Entities;
using ComarchCwiczenia.Services.Invoices;

namespace ComarchCwiczenia.Services.Tests.Invoices
{
    [TestFixture]
    public class InvoiceServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }


        [Test]
        public void CreateItem_Should_CreateCorrectInvoiceItemName()
        {
            // Arrange
            string expected = "Item1";
            InvoiceService sut = new InvoiceService();

            // Act
            InvoiceItem actual = sut.CreateItem(expected, 10, 23);

            // Assert
            Assert.IsNotEmpty(actual.ItemName);
            Assert.AreEqual(expected, actual.ItemName);
        }


        [Test]
        public void CreateItem_Should_CreateCorrectInvoiceItemNetTaxValues()
        {
            // Arrange
            decimal net = 10;
            decimal tax = 23;
            InvoiceService sut = new InvoiceService();

            // Act
            InvoiceItem actual = sut.CreateItem("Item1", net, tax);

            // Assert
            Assert.AreEqual(net, actual.NetValue);
            Assert.That(actual.TaxValue, Is.EqualTo(tax));
        }

        [TestCase(10, 23, 12.3)]
        [TestCase(20, 23, 24.6)]
        public void CreateItem_Should_CreateCorrectInvoceItemWithGrossValue
            (decimal netValue, decimal taxValue, decimal grossValue)
        {
            // Arrange
            InvoiceService sut = new();

            // Act
            InvoiceItem actual = sut.CreateItem("Item1", netValue, taxValue);

            // Assert
            Assert.That(actual.GrossValue, Is.EqualTo(grossValue));
        }

        [Test]
        public void CreateItem_Should_ReturnTheSameInvoiceItem()
        {
            // Arrange
            InvoiceService sut = new();

            // Act
            InvoiceItem item1 = sut.CreateItem("Item1", 10, 23);
            InvoiceItem item2 = item1;

            // Assert
            Assert.AreEqual(item1, item2);

        }

        [Test]
        public void When_ItemNameIsNullOrEmpty_CreateItem_Should_ThrowArgumentException()
        {
            // Arrange
            InvoiceService sut = new();

            // Act & Assert

            Assert.Throws<ArgumentException>(() => sut.CreateItem("", 10, 23));
            Assert.Throws<ArgumentException>(() => sut.CreateItem(null, 10, 23));
        }

    }
}
