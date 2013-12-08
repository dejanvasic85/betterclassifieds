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
        public abstract string RelativePath { get; }

        protected BasePage(IWebDriver webdriver)
        {
            this.WebDriver = webdriver;
        }

        public virtual void InitElements()
        {
            PageFactory.InitElements(this.WebDriver, this);
        }

        public bool IsElementPresentBy(By by)
        {
            return this.WebDriver.FindElements(by).Any();
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
