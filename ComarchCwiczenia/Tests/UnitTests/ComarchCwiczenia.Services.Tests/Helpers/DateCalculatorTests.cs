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

        [Test]
        public void GetNextBusinessDay_Should_HandleLeapYear()
        {
            // Zadanie 1: Test w roku przestępnym, 28 lutego -> Następny dzień roboczy to 29 lutego
            _calculator.GetNextBusinessDay(28.February(2024)).Should().Be(29.February(2024));
        }

        [Test]
        public void GetNextBusinessDay_Should_NotChangeTimePart()
        {
            // Zadanie 2: Test zachowania części czasowej - GetNextBusinessDay nie powinno zmieniać części czasowej (TimeOfDay)
            DateTime time = 23.February(2024).At(15, 30); // 15:30
            _calculator.GetNextBusinessDay(time).TimeOfDay.Should().Be(time.TimeOfDay);
        }

        [Test]
        public void GetNextBusinessDay_ShouldBeAfterInputDate()
        {
            // Zadanie 3: Test, że zwracana data jest zawsze po podanej dacie
            DateTime date = 23.February(2024);
            _calculator.GetNextBusinessDay(date).Should().BeAfter(date);
        }

        [Test]
        public void GetNextBusinessDay_Should_BeCloseToInputDate()
        {
            // Zadanie 4: Test, że zwracana data jest blisko podanej daty (nie więcej niż 3 dni później) (BeCloseTo)
            DateTime date = 23.February(2024);
            _calculator.GetNextBusinessDay(date).Should().BeCloseTo(date, TimeSpan.FromDays(3));
        }

        [Test]
        public void GetNextBusinessDay_Should_BeOnWeekday()
        {
            // Zadanie 5: Test, że zwracana data jest dniem roboczym
            DateTime date = 23.February(2024);
            DateTime nextBusinessDay = _calculator.GetNextBusinessDay(date);
            nextBusinessDay.DayOfWeek.Should().NotBe(DayOfWeek.Saturday).And.NotBe(DayOfWeek.Sunday);
        }
    }
}
