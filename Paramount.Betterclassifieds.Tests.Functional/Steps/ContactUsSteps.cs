using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class ContactUsSteps
    {
        private readonly PageBrowser _pageBrowser;

        public ContactUsSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [Given(@"I navigate to the contact us page")]
        public void GivenINavigateToTheContactUsPage()
        {
            _pageBrowser.NavigateTo<ContactUsPage>();
        }

        [When(@"I submit the contact us form")]
        public void WhenISubmitTheContactUsForm()
        {
            _pageBrowser.Init<ContactUsPage>().Submit();
        }

        [Then(@"I should see a required validation message for First Name, Email and Comment")]
        public void ThenIShouldSeeARequiredValidationMessageForFirstNameEmailAndComment()
        {
            var page = _pageBrowser.Init<ContactUsPage>();
            Assert.That( page.IsFirstNameRequiredMsgShown(), Is.True );
            Assert.That( page.IsEmailRequiredMsgShown(), Is.True);
            Assert.That(page.IsRequiredPhoneMsgShown(), Is.True);
        }

        [When(@"I provide my comment and contact details")]
        public void WhenIProvideMyCommentAndContactDetails()
        {
            _pageBrowser.Init<ContactUsPage>()
                .WithFullName()
                .WithEmail()
                .WithPhone()
                .WithComments();
        }

        [Then(@"I should see a human test validation message")]
        public void ThenIShouldSeeAHumanTestValidationMessage()
        {
            Assert.That(_pageBrowser.Init<ContactUsPage>().IsHumanTestValidationMsgShown(), Is.True);
        }
    }
}