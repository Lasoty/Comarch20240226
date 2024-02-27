using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Services.Invoices;
using FluentAssertions;
using Moq;
using Range = Moq.Range;

namespace ComarchCwiczenia.Services.Tests.Invoices
{
    [TestFixture]
    public class InvoiceServiceMoqTests
    {
        private Mock<ITaxService> _taxServiceMock;
        private Mock<IDiscountService> _discountServiceMock;
        private InvoiceService _invoiceService;

        [OneTimeSetUp]

        [SetUp]
        public void Setup()
        {
            _taxServiceMock = new Mock<ITaxService>();
            _discountServiceMock = new Mock<IDiscountService>();

            _invoiceService = new InvoiceService(_taxServiceMock.Object, _discountServiceMock.Object);
        }

        [Test]
        public void CalculateTotal_Should_ReturnsExpectedTotal()
        {
            // arrange
            var amount = 100;
            var customerType = "Regular";

            _discountServiceMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
                .Returns(10);

            _taxServiceMock.Setup(x => x.GetTax(It.IsAny<decimal>())).Returns(5);

            // act
            var total = _invoiceService.CalculateTotal(amount, customerType);

            // assert
            total.Should().Be(95m);
        }

        [Test]
        public void CalculateTotal_Should_ThrowExeption_WhenAmountLessThan0()
        {
            // arrange
            var amount = -1;
            var customerType = "Regular";

            _discountServiceMock.Setup(x => x.CalculateDiscount(It.IsInRange(decimal.MinValue, 0, Range.Exclusive), It.IsAny<string>()))
                .Throws(new Exception("Błąd obliczania zniżki."));

            // assert
            _invoiceService.Invoking(ic => ic.CalculateTotal(amount, customerType))
                .Should().Throw<Exception>().WithMessage("Błąd obliczania zniżki.");
        }

        [Test]
        public void CalculateTotal_WhenCalled_VerifiesTaxServiceGetTaxIsCalled()
        {
            // arrange
            var amount = 100;
            var customerType = "Regular";

            _discountServiceMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
                .Returns(10);

            _taxServiceMock.Setup(x => x.GetTax(It.IsAny<decimal>())).Returns(5).Verifiable();

            // act

            var total = _invoiceService.CalculateTotal(amount, customerType);

            // assert
            _taxServiceMock.Verify(x => x.GetTax(It.IsAny<decimal>()), Times.Once);
        }

        [Test]
        public void CalculateDiscount_WhenCalled_UsesCallbackToManipulateAmount()
        {
            decimal capturedDiscount = 0;

            _discountServiceMock.Setup(x => x.CalculateDiscount(It.IsAny<decimal>(), It.IsAny<string>()))
                .Returns(10)
                .Callback<decimal, string>((amount, _) => capturedDiscount = amount * 0.1m);

            _invoiceService.CalculateTotal(100, "Regular");

            capturedDiscount.Should().Be(10);
        }
    }
}
