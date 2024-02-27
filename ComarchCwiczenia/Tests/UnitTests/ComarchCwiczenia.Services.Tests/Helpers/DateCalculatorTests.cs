using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComarchCwiczenia.Services.Helpers;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace ComarchCwiczenia.Services.Tests.Helpers
{
    [TestFixture]
    public class DateCalculatorTests
    {
        private DateCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new DateCalculator();
        }

        [Test]
        public void GetNextBusinessDay_Should_SkipWeekends()
        {
            DateTime actual = _calculator.GetNextBusinessDay(24.February(2024));

            actual.Should().Be(26.February(2024));

            _calculator.GetNextBusinessDay(23.February(2024)).Should().Be(26.February(2024));
            _calculator.GetNextBusinessDay(25.February(2024)).Should().Be(26.February(2024));
            _calculator.GetNextBusinessDay(27.February(2024)).Should().Be(28.February(2024));
        }
    }
}
