using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
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

        public IWebElement FindElement(By by)
        {
            return WebDriver.FindElement(by);
        }
    }
}
