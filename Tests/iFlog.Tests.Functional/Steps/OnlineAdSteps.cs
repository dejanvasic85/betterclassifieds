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
        private readonly Mocks.IDataManager _dataManager;
        private Router _router;

        public OnlineAdSteps(IWebDriver webDriver, IConfig configuration, Mocks.IDataManager dataManager) : base(webDriver, configuration)
        {
            _onlineAdPage = new Pages.OnlineAdPage(webDriver);
            _dataManager = dataManager;
        }

        [Given(@"The online ad titled ""(.*)""")]
        public void GivenTheOnlineAdTitled(string adTitle)
        {
            _dataManager.CreateAd(adTitle);
        }

        [When(@"I navigate to ""(.*)""")]
        public void WhenINavigate(string url)
        {
            Browser.Navigate().GoToUrl(string.Concat(Configuration.BaseUrl, url));
        }
        
        [Then(@"the page title should start with ""(.*)""")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            Assert.IsTrue(Browser.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
