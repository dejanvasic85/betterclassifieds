using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class EditAdSteps
    {
        private readonly UserContext _userContext;
        private readonly AdContext _adContext;
        private readonly PageBrowser _pageBrowser;
        
        public EditAdSteps(UserContext userContext, AdContext adContext, PageBrowser pageBrowser)
        {
            _userContext = userContext;
            _adContext = adContext;
            _pageBrowser = pageBrowser;
        }

        [When(@"I select to edit the newly placed ad")]
        public void WhenISelectToEditTheNewlyPlacedAd()
        {
            _pageBrowser.Init<UserAdsPage>().EditAd(_adContext.AdId);
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