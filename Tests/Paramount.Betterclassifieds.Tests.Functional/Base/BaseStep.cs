using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    [Binding]
    public abstract class BaseStep
    {
        public readonly IWebDriver WebDriver;
        public readonly IConfig Configuration;
        public readonly TestRouter Router;

        protected BaseStep(IWebDriver webDriver, IConfig configuration)
        {
            this.WebDriver = webDriver;
            this.Configuration = configuration;
            this.Router = new TestRouter(webDriver, configuration);
        }

        protected TPage Resolve<TPage>()
        {
            return (TPage) Activator.CreateInstance(typeof (TPage), args: WebDriver);
        }
    }
}