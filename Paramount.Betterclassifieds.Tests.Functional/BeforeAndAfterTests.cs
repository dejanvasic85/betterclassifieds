using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using System;
using System.Drawing.Imaging;
using System.IO;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional
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
            _container.RegisterInstanceAs(DataRepositoryFactory.Create(), typeof(ITestDataRepository));
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
            _container.RegisterInstanceAs(new DapperDataRepository(), typeof(ITestDataRepository));
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

            screenshot.SaveAsFile(String.Format("{0}\\{1}.jpg",
                screenShotDir,
                TestContext.CurrentContext.Test.Name), ImageFormat.Jpeg);

            File.WriteAllText(string.Format("{0}\\{1}.html",
                screenShotDir,
                TestContext.CurrentContext.Test.Name), _webDriver.PageSource);
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

        #region Data Setup
        
        [BeforeFeature("booking")]
        public static void SetupBookingFeature()
        {
            // Use the dapper manager to initialise some baseline test data for our booking scenarios
            ITestDataRepository dataRepository = DataRepositoryFactory.Create();
            /*
            // Online Publication  ( this should be removed later - no such thing as online publication ! )
            dataRepository.AddPublicationIfNotExists(TestData.OnlinePublication, Constants.PublicationType.Online, frequency: "Online", frequencyValue: null);
            dataRepository.AddPublicationAdTypeIfNotExists(TestData.OnlinePublication, Constants.AdType.OnlineAd);

            // Print Publication
            dataRepository.AddPublicationIfNotExists(TestData.SeleniumPublication);
            dataRepository.AddPublicationAdTypeIfNotExists(TestData.SeleniumPublication, Constants.AdType.LineAd);
            dataRepository.AddEditionsToPublication(TestData.SeleniumPublication, 50);*/

            // Categories ( assign to each publication automatically )
            dataRepository.AddCategoryIfNotExists(TestData.SubCategory, TestData.ParentCategory);
            
            // Rates
            dataRepository.AddOnlineRateForCategoryIfNotExists(price : 0, categoryName: TestData.SubCategory);
            
            // Location and Area
            dataRepository.AddLocationIfNotExists(parentLocation: TestData.Location_Any, areas: TestData.LocationArea_Any);
            dataRepository.AddLocationIfNotExists(TestData.Location_Australia, TestData.Location_Victoria, "Melbourne"); 

            // Drop any existing user network
            dataRepository.DropUserNetwork(TestData.DefaultUsername);
        }

        [BeforeFeature("booking", "extendbooking")]
        public static void AddMembershipUser()
        {
            var dataRepository = DataRepositoryFactory.Create();

            // Setup a demo user
            dataRepository.AddUserIfNotExists(TestData.DefaultUsername, TestData.DefaultPassword, TestData.UserEmail, RoleType.Advertiser);
        }
        
        [BeforeFeature("usernetwork")]
        public static void SetupUserNetworkFeature()
        {
            var dataRepository = DataRepositoryFactory.Create();
            dataRepository.DropUserNetwork(TestData.DefaultUsername);
        }
        #endregion
    }
}