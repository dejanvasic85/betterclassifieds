using System;
using System.Configuration;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
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

        [BeforeScenario("web")]
        public void RegisterSeleniumDriver()
        {
            IWebDriver driver;

            switch (ConfigurationManager.AppSettings["Browser"].ToLower())
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
            _container.RegisterInstanceAs(driver, typeof(IWebDriver));
        }

        [AfterScenario("web")]
        public void DisposeSeleniumWebDriver()
        {
            _container.Resolve<IWebDriver>().Dispose();
        }

        [BeforeScenario]
        public void SetupTestDataRepository()
        {
            _container.RegisterInstanceAs(new TestConfiguration(), typeof(IConfig));
            _container.RegisterInstanceAs(new DapperDataManager(), typeof(ITestDataManager));
        }

        [BeforeFeature("booking")]
        public static void SetupBookingFeature()
        {
            var dataManager = new DapperDataManager();

            dataManager.AddAdTypeIfNotExists(Constants.AdType.LineAd);
            dataManager.AddAdTypeIfNotExists(Constants.AdType.OnlineAd);
            dataManager.AddPublicationIfNotExists("Selenium Publication");
            dataManager.AddOnlinePublicationIfNotExists();
            dataManager.AddCategoryIfNotExists("Selenium Child", "Selenium Parent");
        }
    }
}