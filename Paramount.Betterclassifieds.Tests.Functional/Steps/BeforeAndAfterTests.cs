using System.Configuration;
using System.Web.UI;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using System.Drawing.Imaging;
using System.IO;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BeforeAndAfterTests
    {
        private readonly IObjectContainer _container;

        private static IWebDriver _webDriver;
        private static TestConfiguration _configuration;

        public BeforeAndAfterTests(IObjectContainer container)
        {
            _container = container;
            _container.RegisterInstanceAs(new PageBrowser(_webDriver, _configuration), typeof(PageBrowser));
            _container.RegisterInstanceAs(new AdminPageBrowser(_webDriver, _configuration), typeof(AdminPageBrowser));
        }

        [BeforeTestRun]
        public static void SetupPageBrowser()
        {
            _configuration = new TestConfiguration();
            _webDriver = GetDriverForBrowser(_configuration.BrowserType);
        }

        [BeforeScenario]
        public void RegisterConfigAndRepository()
        {
            _container.RegisterInstanceAs(new TestConfiguration(), typeof(IConfig));
            _container.RegisterInstanceAs(new DapperDataManager(), typeof(ITestDataManager));
             _webDriver.Manage().Cookies.DeleteAllCookies();
        }

        [AfterTestRun]
        public static void CloseBrowser()
        {
            _webDriver.Dispose();
        }

        [AfterStep]
        public void TakeScreenshotForWebTestFailure()
        {
            if (ScenarioContext.Current.TestError == null)
                return;

            // Max the size and take a screenshot of it.
            _webDriver.Manage().Window.Maximize();

            Screenshot screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();

            const string screenShotDir = "Screenshots";
            if (!Directory.Exists(screenShotDir))
                Directory.CreateDirectory(screenShotDir);

            screenshot.SaveAsFile(string.Format("{0}\\{1}.jpg",
                screenShotDir,
                TestContext.CurrentContext.Test.Name), ImageFormat.Jpeg);
        }

        private static IWebDriver GetDriverForBrowser(string browserName)
        {
            IWebDriver driver;
            switch (browserName)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("test-type");
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            return driver;
        }
    }
}