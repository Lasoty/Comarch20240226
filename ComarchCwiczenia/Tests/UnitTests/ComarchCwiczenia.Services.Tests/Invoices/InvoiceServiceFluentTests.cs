using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Domain.Entities;
using ComarchCwiczenia.Services.Invoices;
using FluentAssertions;
using FluentAssertions.Events;

namespace ComarchCwiczenia.Services.Tests.Invoices
{
    [TestFixture]
    public class InvoiceServiceFluentTests
    {
        private InvoiceService _invoiceService;

        [SetUp]
        public void Setup()
        {
            _invoiceService = new InvoiceService();
        }

        #region Assert strings
        [Test]
        public void GenerateInvoiceNumber_Should_StartWhith_FV()
        {
            string actual = _invoiceService.GenerateInvoiceNumber();

            actual.Should().NotBeNullOrEmpty().And.StartWith("FV");
        }

        [Test]
        public void GenerateInvoiceNumber_Should_ContainCurrentDate()
        {
            string currentDate = DateTime.Now.ToString("yyyyMM");

            string actual = _invoiceService.GenerateInvoiceNumber();

            actual.Should().Contain(currentDate);
        }

        [Test]
        public void GenerateInvoiceNumber_Should_HaveCorrectFormat()
        {
            string actual = _invoiceService.GenerateInvoiceNumber();

            actual.Should().MatchRegex(@"FV-\d{6}-\d{3}$");
        }

        [Test]
        public void GenerateInvoiceNumber_ShouldMatch_ExpectedFormat()
        {
            var invoiceNumber = _invoiceService.GenerateInvoiceNumber();

            invoiceNumber.Should().Match("FV-??????-???").And.BeOfType<string>();
        }

        [Test]
        public void GenerateInvoiceNumber_ShouldHaveLength_Of_17()
        {
            var invoiceNumber = _invoiceService.GenerateInvoiceNumber();

            invoiceNumber.Should().HaveLength(13);
        }
        #endregion

        #region Assert collections

        [Test]
        public void GenerateInvoiceItems_Should_Return_NonEmptyCollection()
        {
            IEnumerable<InvoiceItem> actual = _invoiceService.GenerateInvoiceItems();

            actual.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void GenerateInvoiceItems_Should_Return_ItemWithSpecificName()
        {
            IEnumerable<InvoiceItem> actual = _invoiceService.GenerateInvoiceItems();

            actual.Should().Contain(item => item.ItemName.Equals("item 1", StringComparison.InvariantCultureIgnoreCase));
        }

        [Test]
        public void GenerateInvoiceItems_Should_Have_ItemsInAscedingOrderByQuantity()
        {
            IEnumerable<InvoiceItem> actual = _invoiceService.GenerateInvoiceItems();

            actual.Should().BeInAscendingOrder(item => item.Quantity);
        }

        #endregion

        #region Assert Events

        [Test]
        public void CreateInvoice_Should_RaiseInvoiceCreatedEvent()
        {
            using IMonitor<InvoiceService>? monitoredSubject = _invoiceService.Monitor();

            Invoice actual = _invoiceService.CreateInvoice(Enumerable.Empty<InvoiceItem>());
            monitoredSubject.Should().Raise(nameof(InvoiceService.InvoiceCreated));
        }

        #endregion

        #region Assert Exceptions

        [Test]
        public void CreateItem_Should_ThrowException_When_NameIsNullOrEmpty()
        {
            _invoiceService.Invoking(ic => ic.CreateItem("", 10, 23)).Should().Throw<ArgumentException>()
                .WithMessage("Nazwa elementu nie może być pusta. (Parameter 'name')");
        }

        #endregion
    }
}
