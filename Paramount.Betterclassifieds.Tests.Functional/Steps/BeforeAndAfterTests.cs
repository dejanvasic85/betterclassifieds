using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
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

        /// <summary>
        /// All scenarios will require some implementations injected
        /// </summary>
        [BeforeScenario]
        public void RegisterConfigAndRepository()
        {
            _container.RegisterInstanceAs(new TestConfiguration(), typeof (IConfig));
            _container.RegisterInstanceAs(new DapperDataManager(), typeof (ITestDataManager));
        }

        [BeforeScenario("web")]
        public void RegisterSeleniumDriver()
        {
            IConfig config = _container.Resolve<IConfig>();

            // Create a web driver for all web tests
            IWebDriver driver = GetDriverForBrowser(config.BrowserType);
            _container.RegisterInstanceAs(driver, typeof (IWebDriver));

            // Create instance and register for the page factory
            _container.RegisterInstanceAs(new PageBrowser(driver, config), typeof (PageBrowser));
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
            ITestDataManager dataManager = new DapperDataManager();

            // Online Publication  ( this should be removed later - no such thing as online publication ! )
            dataManager.AddPublicationIfNotExists(TestData.OnlinePublication, Constants.PublicationType.Online, frequency: "Online", frequencyValue: null);
            dataManager.AddPublicationAdTypeIfNotExists(TestData.OnlinePublication, Constants.AdType.OnlineAd);

            // Print Publication
            dataManager.AddPublicationIfNotExists(TestData.SeleniumPublication);
            dataManager.AddPublicationAdTypeIfNotExists(TestData.SeleniumPublication, Constants.AdType.LineAd);
            dataManager.AddEditionsToPublication(TestData.SeleniumPublication, 50);

            // Categories ( assign to each publication automatically )
            dataManager.AddCategoryIfNotExists(TestData.SubCategory, TestData.ParentCategory);

            // Ratecard
            dataManager.AddRatecardIfNotExists("Selenium Free Rate", 0, 0, TestData.SubCategory);

            // Location and Area
            dataManager.AddLocationIfNotExists("Australia", "Victoria", "Melbourne");
        }

        [BeforeFeature("booking", "extendbooking")]
        public static void SetupBookingExtensionFeature()
        {
            ITestDataManager dataManager = new DapperDataManager();
            
            // Setup a demo user
            dataManager.AddUserIfNotExists(TestData.Username, TestData.Password, TestData.UserEmail);
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

        [AfterStep("web")]
        public void ScreenshotOnError()
        {
            //Add a line break in the Console output for better readability
            Console.WriteLine();

            var config = _container.Resolve<IConfig>();
            var webdriver = _container.Resolve<IWebDriver>();
            
            if (ScenarioContext.Current.TestError == null 
                || string.IsNullOrEmpty(config.ErrorEmail)
                || config.SendScreenshotOneError == false)
                return;

            Screenshot screenshot = ((ITakesScreenshot) webdriver).GetScreenshot();
            Stream screenshotStream = new MemoryStream(screenshot.AsByteArray);

            Stream pageSourceStream = new MemoryStream();
            var writer = new StreamWriter(pageSourceStream);
            writer.Write(webdriver.PageSource);
            writer.Flush();
            pageSourceStream.Position = 0;

            var msg = new MailMessage("smoketest@paramountit.com.au", config.ErrorEmail)
                {
                    Subject = "Specflow Scenario failed: " + ScenarioContext.Current.ScenarioInfo.Title
                };
            msg.Attachments.Add(new Attachment(screenshotStream, ScenarioContext.Current.ScenarioInfo.Title + ".png", null));
            msg.Attachments.Add(new Attachment(pageSourceStream, webdriver.Title + ".html"));

            var plainTextEmailBody = ScenarioContext.Current.TestError.Message;

            var resource = new LinkedResource(screenshotStream);
            var htmlEmailBody = string.Format("<p>{0}</p><img src=\"cid:{1}\">", ScenarioContext.Current.TestError.Message, resource.ContentId);

            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(plainTextEmailBody, null, MediaTypeNames.Text.Plain));
            var htmlView = AlternateView.CreateAlternateViewFromString(htmlEmailBody, null, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(resource);
            msg.AlternateViews.Add(htmlView);

            var smtp = new SmtpClient();
            smtp.Send(msg);
        }
    }
}