using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Services.Invoices;
using FluentAssertions;

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
        
    }
}
