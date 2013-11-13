using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using iFlog.Tests.Functional.Pages;

namespace iFlog.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps : BaseStep
    {
        private readonly Pages.OnlineAdPage onlineAdPage;
        private readonly Mocks.IDataManager dataManager;
        private readonly TestRouter testRouter;

        public OnlineAdSteps(IWebDriver webDriver, IConfig configuration, Mocks.IDataManager dataManager, TestRouter testRouter)
            : base(webDriver, configuration)
        {
            onlineAdPage = new Pages.OnlineAdPage(webDriver);
            this.dataManager = dataManager;
            this.testRouter = testRouter;
        }

        [Given(@"The online ad titled ""(.*)""")]
        public void GivenTheOnlineAdTitled(string adTitle)
        {
            int addOrUpdateOnlineAd = dataManager.AddOrUpdateOnlineAd(adTitle);
            ScenarioContext.Current.Add("AdId", addOrUpdateOnlineAd);
        }

        [Given(@"The online ad titled ""(.*)""")]
        public void GivenTheOnlineAdTitled(Table adTitle)
        {
            
        }

        [When(@"I navigate to ""(.*)""")]
        public void WhenINavigate(string url)
        {
            string urlWithId = string.Format(url, ScenarioContext.Current.Get<int>("AdId"));
            testRouter.NavigateTo(urlWithId);
        }

        [Then(@"the page title should start with ""(.*)""")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            Assert.IsTrue(Browser.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));
            
        }

        [Then(@"the online ad contact name should be ""(.*)""")]
        public void ThenTheOnlineAdContactNameShouldBe(string sampleContact)
        {
            Assert.AreEqual(sampleContact, onlineAdPage.GetContactName());
        }

    }
}
