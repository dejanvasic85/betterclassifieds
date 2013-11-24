using System;
using System.Configuration;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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

    }
}