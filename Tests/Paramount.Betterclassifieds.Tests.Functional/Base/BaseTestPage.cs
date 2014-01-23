using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public abstract class BaseTestPage
    {
        public readonly IWebDriver WebDriver;
        private readonly IConfig _config;

        protected BaseTestPage(IWebDriver webdriver, IConfig config)
        {
            this.WebDriver = webdriver;
            _config = config;
        }

        public virtual string GetTitle()
        {
            return WebDriver.Title;
        }

        public virtual void InitElements()
        {
            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(WebDriver, this);

            // Ensure the window size is good ( really useful for the testing in "offline" mode )
            WebDriver.Manage().Window.Size = new System.Drawing.Size(1200, 768);
        }

        public bool IsElementPresentBy(By by)
        {
            return WebDriver.FindElements(by).Any();
        }

        public IWebElement FindElement(By by, int maxSecondsToTimeout = 30)
        {
            // Wait for element to appear first (just in case)
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(maxSecondsToTimeout));
            wait.Until(ExpectedConditions.ElementIsVisible(by));

            return WebDriver.FindElement(by);
        }

        public bool IsDisplayed()
        {
            var fullUrl = _config.BaseUrl + GetType().GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            return WebDriver.Url.StartsWith(fullUrl, StringComparison.OrdinalIgnoreCase);
        }
    }
}
