using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace iFlog.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps : BaseStep
    {
        private readonly Pages.OnlineAdPage _onlineAdPage;
        private Router _router;

        public OnlineAdSteps(IWebDriver webDriver, IConfig configuration) : base(webDriver, configuration)
        {
            _onlineAdPage = new Pages.OnlineAdPage(webDriver);
        }

        [Given(@"The online ad titled ""(.*)""")]
        public void GivenTheOnlineAdTitled(string adTitle)
        {
            // todo - setup mock data
        }

        [When(@"I navigate ""(.*)""")]
        public void WhenINavigate(string url)
        {
            WebDriver.Navigate().GoToUrl(string.Concat(Configuration.BaseUrl, url));
        }
        
        [Then(@"the page title should start with ""(.*)""")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            Assert.IsTrue(WebDriver.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
