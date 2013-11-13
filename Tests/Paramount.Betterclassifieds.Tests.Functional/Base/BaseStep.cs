using System;
using System.Configuration;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace iFlog.Tests.Functional
{
    [Binding]
    public abstract class BaseStep
    {
        public readonly IWebDriver Browser;
        public readonly IConfig Configuration;
        public readonly TestRouter Router;

        protected BaseStep(IWebDriver webDriver, IConfig configuration)
        {
            this.Browser = webDriver;
            this.Configuration = configuration;
            this.Router = new TestRouter(webDriver);
        }

    }
}