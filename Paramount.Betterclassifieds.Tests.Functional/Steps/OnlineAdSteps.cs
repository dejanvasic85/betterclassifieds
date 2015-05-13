using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using System;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class OnlineAdSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _dataRepository;
        private readonly UserContext _userContext;
        private readonly AdContext _adContext;

        public OnlineAdSteps(PageBrowser pageBrowser, ITestDataRepository dataRepository, UserContext userContext, AdContext adContext)
        {
            _pageBrowser = pageBrowser;
            _dataRepository = dataRepository;
            _userContext = userContext;
            _adContext = adContext;
        }

        [Given(@"I have an online ad titled ""(.*)"" in parent category ""(.*)"" and sub category ""(.*)""")]
        public void GivenTheOnlineADTitledInParentCategoryAndSubCategory(string adTitle, string parentCategory, string childCategory)
        {
            // Call the given that creates the categories first
            GivenParentCategoryAndSubCategory(parentCategory, childCategory);
            GivenLocationAndArea(TestData.Location_Australia, TestData.Location_Victoria);

            int? adId = _dataRepository.DropCreateOnlineAd(adTitle, parentCategory, childCategory, _userContext.Username);

            ScenarioContext.Current.Add("AdId", adId);
            _adContext.AdId = adId.GetValueOrDefault();
        }
        
        [Given(@"The parent category ""(.*)"" and sub category ""(.*)""")]
        public void GivenParentCategoryAndSubCategory(string parentCategory, string childCategory)
        {
            _dataRepository.AddCategoryIfNotExists(childCategory, parentCategory);
        }

        [Given(@"The location ""(.*)"" and sub area ""(.*)""")]
        public void GivenLocationAndArea(string location, string area)
        {
            _dataRepository.AddLocationIfNotExists(location, area);
        }

        [When(@"I navigate to online ad ""(.*)""")]
        public void WhenINavigate(string url)
        {
            string urlWithId = string.Format(url, _adContext.AdId);

            // Route to an exact URL becuase this is what we're testing!
            _pageBrowser.NavigateTo(urlWithId);
        }

        [Then(@"the page title should start with ""(.*)""")]
        public void ThenThePageTitleShouldStartWith(string title)
        {
            var onlineAdPage = _pageBrowser.Init<OnlineAdTestPage>(false);
            Assert.IsTrue(onlineAdPage.GetTitle().StartsWith(title, StringComparison.OrdinalIgnoreCase));
        }

        [Then(@"the online ad contact name should be ""(.*)""")]
        public void ThenTheOnlineAdContactNameShouldBe(string sampleContact)
        {
            OnlineAdTestPage onlineAdTestPage = _pageBrowser.Init<OnlineAdTestPage>(false);
            Assert.AreEqual(sampleContact, onlineAdTestPage.GetContactName());
        }

        [Then(@"the online ad contact email should be ""(.*)""")]
        public void ThenTheOnlineAdContactEmailShouldBe(string expected)
        {
            OnlineAdTestPage onlineAdTestPage = _pageBrowser.Init<OnlineAdTestPage>(false);
            Assert.That(onlineAdTestPage.GetContactEmail(), Is.EqualTo(expected));
        }

        [Then(@"the online ad contact phone should be ""(.*)""")]
        public void ThenTheOnlineAdContactPhoneShouldBe(string expected)
        {
            OnlineAdTestPage onlineAdTestPage = _pageBrowser.Init<OnlineAdTestPage>(false);
            Assert.That(onlineAdTestPage.GetContactPhone(), Is.EqualTo(expected));
        }

    }
}
