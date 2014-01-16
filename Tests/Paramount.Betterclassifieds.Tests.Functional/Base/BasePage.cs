using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public abstract class BasePage
    {
        public readonly IWebDriver WebDriver;

        protected BasePage(IWebDriver webdriver)
        {
            this.WebDriver = webdriver;
        }

        public virtual string GetTitle()
        {
            return WebDriver.Title;
        }

        public virtual void InitElements()
        {
            PageFactory.InitElements(WebDriver, this);

            // Ensure the window size is good ( really useful for the testing in "offline" mode )
            WebDriver.Manage().Window.Size = new System.Drawing.Size(1400, 1100);
        }

        public bool IsElementPresentBy(By by)
        {
            return WebDriver.FindElements(by).Any();
        }

        public IWebElement FindElement(By by, int maxSecondsToTimeout = 5)
        {
            // Wait for element to appear first (just in case)
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(maxSecondsToTimeout));
            wait.Until(ExpectedConditions.ElementIsVisible(by));

            return WebDriver.FindElement(by);
        }
    }
}
