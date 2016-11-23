using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    [Binding]
    public class StartUpHooks
    {
        private readonly IObjectContainer _container;

        // This should be the only instance of the Selenium WebDriver
        private static IWebDriver _webDriver;
        private static IConfig _configuration;

        public StartUpHooks(IObjectContainer container)
        {
            _container = container;
            _container.RegisterInstanceAs(new PageBrowser(_webDriver, _configuration), typeof(PageBrowser));
            _container.RegisterInstanceAs(DataRepositoryFactory.Create(_configuration), typeof(ITestDataRepository));
            _container.RegisterInstanceAs(new ContextData<AdBookingContext>(), typeof(ContextData<AdBookingContext>));
        }

        [BeforeTestRun]
        public static void SetupPageBrowser()
        {
            _configuration = ConfigFactory.CreateConfig();
            _webDriver = GetDriverForBrowser("firefox");
        }
        
        [BeforeScenario]
        public void RegisterConfigAndRepository()
        {
            var config = ConfigFactory.CreateConfig();
            var connectionFactory = new ConnectionFactory(config);

            _container.RegisterInstanceAs(config, typeof(IConfig));
            _container.RegisterInstanceAs(connectionFactory, typeof(ConnectionFactory));
            _container.RegisterInstanceAs(new DapperDataRepository(connectionFactory), typeof(ITestDataRepository));

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

            Screenshot screenshot = ((ITakesScreenshot)_webDriver).GetScreenshot();

            const string screenShotDir = "Screenshots";
            if (!Directory.Exists(screenShotDir))
                Directory.CreateDirectory(screenShotDir);

            screenshot.SaveAsFile($"{screenShotDir}\\{TestContext.CurrentContext.Test.Name}.jpg", ImageFormat.Jpeg);
            File.WriteAllText($"{screenShotDir}\\{TestContext.CurrentContext.Test.Name}.html", _webDriver.PageSource);
        }

        private static IWebDriver GetDriverForBrowser(string browserName)
        {
            IWebDriver driver;
            switch (browserName)
            {
                case "chrome":
                    //var chromeOptions = new ChromeOptions();
                    //chromeOptions.AddArgument("test-type");
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    //var firefoxBinary = new FirefoxBinary(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe");
                    //var firefoxProfile = new FirefoxProfile();
                    //driver = new FirefoxDriver(firefoxBinary, firefoxProfile);
                    driver = new FirefoxDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                default:
                    driver = new FirefoxDriver();
                    break;
            }
            driver.Manage().Window.Size = new Size(1290, 800);
            return driver;
        }

        #region Data Setup

        [BeforeFeature("booking", "eventAd")]
        public static void SetupBookingFeature()
        {
            // Use the dapper manager to initialise some baseline test data for our booking scenarios
            var dataRepository = DataRepositoryFactory.Create(_configuration);

            dataRepository.AddPublicationIfNotExists(TestData.SeleniumPublication);

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
            dataRepository.AddCategoryIfNotExists(TestData.SubEventCategory, TestData.ParentEventCategory, "Event");

            // Rates
            dataRepository.AddOnlineRateForCategoryIfNotExists(price: 0, categoryName: TestData.SubCategory);
            dataRepository.AddPrintRateForCategoryIfNotExists(TestData.SubCategory);

            // Location and Area
            dataRepository.AddLocationIfNotExists(parentLocation: TestData.Location_Any, areas: TestData.LocationArea_Any);
            dataRepository.AddLocationIfNotExists(TestData.Location_Australia, TestData.Location_Victoria, "Melbourne");

            dataRepository.DropUserNetwork(TestData.DefaultUsername, "ade@spurs.com");
        }

        [BeforeFeature("booking", "eventAd", "extendbooking", "bookEventTickets", "bookTicketsFromInvite")]
        public static void AddMembershipUser()
        {
            var dataRepository = DataRepositoryFactory.Create(_configuration);

            // Setup a demo user
            dataRepository.DropCreateUser(TestData.DefaultUsername, TestData.DefaultPassword, TestData.UserEmail, RoleType.Advertiser);
            dataRepository.DropCreateUser("bddTicketBuyer", "bddTicketBuyer", "bdd@TicketBuyer.com", RoleType.Advertiser);
        }

        #endregion
    }
}