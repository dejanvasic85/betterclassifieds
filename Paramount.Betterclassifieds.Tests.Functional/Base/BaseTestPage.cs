﻿using OpenQA.Selenium;
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

        protected bool IsElementPresentBy(By by)
        {
            return WebDriver.FindElements(by).Any();
        }

        public IWebElement FindElement(By by, int maxSecondsToTimeout = 30)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(maxSecondsToTimeout));
            var element = wait.Until(drv => drv.FindElement(by));
         
            // Scroll to this element because Chrome looks to be a little fussy acting on it if not in view.
            var jsExecutor = (WebDriver as IJavaScriptExecutor);
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView(true)", element);

            return element;
        }

        public IWebElement FindElement(params string[] identifiers)
        {
            foreach (var id in identifiers)
            {
                var element = WebDriver.FindElements(By.Id(id)).FirstOrDefault();

                if (element != null)
                    return element;
            }
            throw new NoSuchElementException("Unable to locate any of the identifiers in the web page.");
        }

        public bool IsDisplayed()
        {
            var fullUrl = _config.BaseUrl + GetType().GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            return WebDriver.Url.StartsWith(fullUrl, StringComparison.OrdinalIgnoreCase);
        }
    }
}
