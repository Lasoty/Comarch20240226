using System.Diagnostics;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WpfCalculator.UI.Tests
{
    public class Tests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServerUri = "http://127.0.0.1:4723";
        private string appPath;
        private Process winAppDriverServerProcess;

        [OneTimeSetUp]
        public void GeneralSetup()
        {
            RunWebDriverServer();

            appPath = Path.Combine(Environment.CurrentDirectory, "WpfCalculator.App.exe");

            var driverOption = new AppiumOptions();
            driverOption.AddAdditionalCapability("app", appPath);

            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServerUri), driverOption);
            Assert.That(driver, Is.Not.Null);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [OneTimeTearDown]
        public void GeneralTearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }

            if (winAppDriverServerProcess != null)
            {
                winAppDriverServerProcess.CloseMainWindow();
                winAppDriverServerProcess.Close();
                winAppDriverServerProcess.Dispose();
            }
        }

        private void RunWebDriverServer()
        {
            string[] potentialPaths =
            {
                @"C:\Program Files\Windows Application Driver",
                @"C:\Program Files (x86)\Windows Application Driver",
            };

            foreach (string path in potentialPaths)
            {
                string exePath = Path.Combine(path, "WinAppDriver.exe");
                if (File.Exists(exePath))
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(exePath)
                    {
                        UseShellExecute = true,
                        WorkingDirectory = path,
                        Verb = "runas" //uruchom jako administrator
                    };

                    try
                    {
                        winAppDriverServerProcess = Process.Start(processStartInfo);
                        Console.WriteLine("WinAppDriver started succesfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Nie uda³o siê uruchomiæ {exePath}. B³¹d: {ex.Message}");
                        Assert.Fail($"Nie uda³o siê uruchomiæ {exePath}. B³¹d: {ex.Message}");
                    }

                    return;
                }

                Assert.Fail("Nie znaleziono WinAppDriver.exe w ¿adnej lokalizacji.");
            }
        }

        [Test]
        public void Test_Addition()
        {
            var buttonOne = driver.FindElementByName("1");
            var buttonTwo = driver.FindElementByName("2");
            var buttonPlus = driver.FindElementByName("+");
            var buttonEquals = driver.FindElementByName("=");
            var resultField = driver.FindElementByAccessibilityId("txtResult");

            buttonOne.Click();
            buttonPlus.Click();
            buttonTwo.Click();
            buttonEquals.Click();

            Assert.AreEqual("3", resultField.Text);
        }
    }
}