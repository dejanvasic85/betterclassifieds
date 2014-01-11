using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps : BaseStep
    {
        private readonly Mocks.ITestDataManager dataManager;
        private readonly TestRouter testRouter;

        public OnlineAdSteps(IWebDriver webDriver, IConfig configuration, Mocks.ITestDataManager dataManager, TestRouter testRouter)
            : base(webDriver, configuration)
        {
            this.dataManager = dataManager;
            this.testRouter = testRouter;
        }

        [Given(@"The online ad titled ""(.*)"" in parent category ""(.*)"" and sub category ""(.*)""")]
        public void GivenTheOnlineAdTitledInParentCategoryAndSubCategory(string adTitle, string parentCategory, string childCategory)
        {
            // Call the given that creates the categories first
            GivenParentCategoryAndSubCategory(parentCategory, childCategory);

            int? adId = dataManager.DropAndAddOnlineAd(adTitle, parentCategory, childCategory);

            ScenarioContext.Current.Add("AdId", adId);
        }

        [Given(@"The parent category ""(.*)"" and sub category ""(.*)""")]
        public void GivenParentCategoryAndSubCategory(string parentCategory, string childCategory)
        {
            dataManager.AddCategoryIfNotExists(parentCategory, childCategory);
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
            Assert.IsTrue(WebDriver.Title.StartsWith(title, StringComparison.OrdinalIgnoreCase));

        }

        [Then(@"the online ad contact name should be ""(.*)""")]
        public void ThenTheOnlineAdContactNameShouldBe(string sampleContact)
        {
            Pages.OnlineAdPage onlineAdPage = Resolve<Pages.OnlineAdPage>();
            Assert.AreEqual(sampleContact, onlineAdPage.GetContactName());
        }

    }
}
