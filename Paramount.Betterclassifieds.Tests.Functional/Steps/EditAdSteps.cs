using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class EditAdSteps
    {
        private readonly UserContext _userContext;
        private readonly AdBookingContext _adBookingContext;
        private readonly PageBrowser _pageBrowser;
        
        public EditAdSteps(UserContext userContext, AdBookingContext adBookingContext, PageBrowser pageBrowser)
        {
            _userContext = userContext;
            _adBookingContext = adBookingContext;
            _pageBrowser = pageBrowser;
        }

        [When(@"I select to edit the newly placed ad")]
        public void WhenISelectToEditTheNewlyPlacedAd()
        {
            _pageBrowser.Init<UserAdsPage>().EditAd(_adBookingContext.AdBookingId);
        }

        [When(@"I update the title to ""(.*)""")]
        public void WhenIUpdateTheTitleTo(string newAdTitle)
        {
            _pageBrowser.Init<EditAdPage>()
                .WithTitle(newAdTitle)
                .SubmitChanges();
        }

        [Then(@"I should see a success message")]
        public void ThenIShouldSeeASuccessMessage()
        {
            Assert.That(_pageBrowser.Init<EditAdPage>().DisplaysSuccessMessage(), Is.True);
        }
    }
}