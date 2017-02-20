using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public sealed class UserAdSteps
    {
        private readonly PageBrowser _browser;

        public UserAdSteps(PageBrowser browser)
        {
            _browser = browser;
        }

        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef
        [When(@"I view the user ads page")]
        public void WhenIViewTheUserAdsPage()
        {
            _browser.GoTo<UserAdsPage>();
        }

        [Then(@"I should see the ad ""(.*)""")]
        public void ThenIShouldSeeTheAd(string title)
        {
            var userAdsPage = _browser.Init<UserAdsPage>();
            var currentAdsForUser = userAdsPage.GetAvailableAdsForCurrentUser();
            Assert.That(currentAdsForUser.Any(a => a.Title == title), Is.True);
        }

        [When(@"selecting to manage the ad ""(.*)""")]
        public void WhenSelectingToManageTheAd(string title)
        {
            var userAdsPage = _browser.Init<UserAdsPage>();
            userAdsPage.EditFirstAdWithTitle(title);
        }
    }
}
