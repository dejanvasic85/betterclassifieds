using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using System.Configuration;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class BeforeAndAfterTests
    {
        private readonly IObjectContainer _container;

        public BeforeAndAfterTests(IObjectContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// All scenarios will require some implementations injected
        /// </summary>
        [BeforeScenario]
        public void SetupTestDataRepository()
        {
            _container.RegisterInstanceAs(new TestConfiguration(), typeof(IConfig));
            _container.RegisterInstanceAs(new DapperDataManager(), typeof(ITestDataManager));
        }

        [BeforeScenario("web")]
        public void RegisterSeleniumDriver()
        {
            // Create a web driver for all web tests
            IWebDriver driver = GetDriverForBrowser(ConfigurationManager.AppSettings["Browser"].ToLower());
            _container.RegisterInstanceAs(driver, typeof(IWebDriver));
            
            // Create instance and register for the page factory
            _container.RegisterInstanceAs(new SeleniumPageFactory(driver), typeof(IPageFactory));
        }
        
        [AfterScenario("web")]
        public void DisposeSeleniumWebDriver()
        {
            _container.Resolve<IWebDriver>().Dispose();
        }

        [BeforeFeature("booking")]
        public static void SetupBookingFeature()
        {
            // Use the dapper manager to initialise some baseline test data for our booking scenarios
            var dataManager = new DapperDataManager();

            dataManager.AddAdTypeIfNotExists(Constants.AdType.LineAd);
            dataManager.AddAdTypeIfNotExists(Constants.AdType.OnlineAd);
            dataManager.AddPublicationIfNotExists("Selenium Publication");
            dataManager.AddEditionsToPublication("Selenium Publication", 10);
            dataManager.AddOnlinePublicationIfNotExists();
            dataManager.AddCategoryIfNotExists("Selenium Child", "Selenium Parent");
            dataManager.AddUserIfNotExists("bdduser", "password123", "bdd@somefakeaddress.com");
        }

        private static IWebDriver GetDriverForBrowser(string browserName)
        {
            IWebDriver driver;
            switch (browserName)
            {
                case "chrome":
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