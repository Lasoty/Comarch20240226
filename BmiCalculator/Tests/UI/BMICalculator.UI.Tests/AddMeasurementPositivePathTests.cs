using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BMICalculator.UI.Tests
{
    public class AddMeasurementPositivePathTests : SeleniumTestBase
    {
        [Test]
        public void AddMeasurementPositivePath()
        {
            driver.Navigate().GoToUrl("https://localhost:7250/");
            driver.Manage().Window.Size = new Size(1920, 1050);

            IWebElement element = driver.FindElement(By.LinkText("Pomiar BMI"));
            element.Click();
            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.Id("Weight")).SendKeys("100");
            driver.FindElement(By.Id("Height")).SendKeys("180");

            IWebElement dropdown = driver.FindElement(By.CssSelector(".form-select"));
            dropdown.FindElement(By.XPath("//option[. = 'Metryczny']")).Click();
            driver.FindElement(By.ClassName("btn-primary")).Click();
            
            Thread.Sleep(TimeSpan.FromSeconds(1));
            var results = driver.FindElements(By.XPath("//b[contains(.,\'30.86\')]"));

            Assert.True(results.Count > 0);
        }
    }
}
