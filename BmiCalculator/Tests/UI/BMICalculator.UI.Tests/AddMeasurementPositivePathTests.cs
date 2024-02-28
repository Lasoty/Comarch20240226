using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        [Test]
        public void AddMeasurementNegativePath()
        {
            driver.Navigate().GoToUrl("https://localhost:7250/");
            driver.Manage().Window.Size = new System.Drawing.Size(1920, 1050);
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(driver => driver.FindElements(By.CssSelector(".display-4")).Count > 0);
            }
            driver.FindElement(By.LinkText("Pomiar BMI")).Click();
            driver.FindElement(By.Id("Weight")).SendKeys("1000");
            driver.FindElement(By.Id("Height")).SendKeys("1000");
            {
                var dropdown = driver.FindElement(By.CssSelector(".form-select"));
                dropdown.FindElement(By.XPath("//option[. = 'Imperialny']")).Click();
            }
            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            var elements = driver.FindElements(By.XPath("//span[contains(.,\'The field Weight must be between 1 and 400.\')]"));
            Assert.True(elements.Count > 0);

            elements = driver.FindElements(By.XPath("//span[contains(.,\'The field Height must be between 1 and 250.\')]"));
            Assert.True(elements.Count > 0);
        }
    }
}
