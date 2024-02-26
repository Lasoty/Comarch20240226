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

    }
}
