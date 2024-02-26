using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComarchCwiczenia.Services.Tests
{
    [TestFixture]
    public class OneTimeSetupTests
    {
        private static int _oneTimeSetupCounter = 0;
        private int _setupCounter = 0;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            _oneTimeSetupCounter++;
            Console.WriteLine("GlobalSetup: Ta metoda uruchamia się przed wszystkimi testami");
        }

        [SetUp]
        public void Setup()
        {
            _setupCounter++;
            Console.WriteLine("Setup: Ta metoda uruchamia się przed każdym testem.");
        }

        [Test]
        public void Test1()
        {
            Console.WriteLine($"Test1: _oneTimeSetupCounter = {_oneTimeSetupCounter}");
            Console.WriteLine($"Test1: _setupCounter = {_setupCounter}");

            Assert.AreEqual(1, _oneTimeSetupCounter);
            Assert.AreEqual(1, _setupCounter);
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine($"Test2: _oneTimeSetupCounter = {_oneTimeSetupCounter}");
            Console.WriteLine($"Test2: _setupCounter = {_setupCounter}");

            Assert.AreEqual(1, _oneTimeSetupCounter);
            Assert.AreEqual(1, _setupCounter);
        }

        [TearDown]
        public void TearDown()
        {
            _setupCounter = 0;
        }
    }
}
