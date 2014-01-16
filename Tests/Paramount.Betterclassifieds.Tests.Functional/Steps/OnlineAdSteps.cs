using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using System;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps
    {
        private readonly PageFactory _pageFactory;
        private readonly ITestDataManager _dataManager;

        public OnlineAdSteps(PageFactory pageFactory, ITestDataManager dataManager)
        {
            _pageFactory = pageFactory;
            _dataManager = dataManager;
        }

        [Given(@"The online a d titled ""(.*)"" in parent category ""(.*)"" and sub category ""(.*)""")]
        public void GivenTheOnlineADTitledInParentCategoryAndSubCategory(string adTitle, string parentCategory, string childCategory)
        {
            // Call the given that creates the categories first
            GivenParentCategoryAndSubCategory(parentCategory, childCategory);

            int? adId = _dataManager.DropAndAddOnlineAd(adTitle, parentCategory, childCategory);

            ScenarioContext.Current.Add("AdId", adId);
        }
        
        [Given(@"The parent category ""(.*)"" and sub category ""(.*)""")]
        public void GivenParentCategoryAndSubCategory(string parentCategory, string childCategory)
        {
            _dataManager.AddCategoryIfNotExists(parentCategory, childCategory);
        }

        [When(@"I navigate to ""(.*)""")]
        public void WhenINavigate(string url)
        {
            string urlWithId = string.Format(url, ScenarioContext.Current.Get<int>("AdId"));

            // Route to an exact URL becuase this is what we're testing!
            _pageFactory.NavigateTo(urlWithId);
        }

        [Then(@"the page title should start with ""(.*)""")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            var onlineAdPage = _pageFactory.Resolve<OnlineAdPage>();
            Assert.IsTrue(onlineAdPage.GetTitle().StartsWith(title, StringComparison.OrdinalIgnoreCase));
        }

        [Then(@"the online ad contact name should be ""(.*)""")]
        public void ThenTheOnlineAdContactNameShouldBe(string sampleContact)
        {
            OnlineAdPage onlineAdPage = _pageFactory.Resolve<OnlineAdPage>();
            Assert.AreEqual(sampleContact, onlineAdPage.GetContactName());
        }

    }
}
