using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BMICalculator.UI.Tests
{
    public abstract class SeleniumTestBase
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-gpu");
            //options.AddArgument("--headless=new");

            driver = new ChromeDriver();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
            driver.Dispose();
        }
    }
}