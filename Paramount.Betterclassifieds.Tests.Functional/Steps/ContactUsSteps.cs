using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class ContactUsSteps
    {
        private readonly PageFactory _pageFactory;

        public ContactUsSteps(PageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        [Given(@"I navigate to the contact us page")]
        public void GivenINavigateToTheContactUsPage()
        {
            _pageFactory.NavigateTo<ContactUsPage>();
        }

        [When(@"I submit the contact us form")]
        public void WhenISubmitTheContactUsForm()
        {
            _pageFactory.Init<ContactUsPage>().Submit();
        }

        [Then(@"I should see a required validation message for First Name, Email and Comment")]
        public void ThenIShouldSeeARequiredValidationMessageForFirstNameEmailAndComment()
        {
            var page = _pageFactory.Init<ContactUsPage>();
            Assert.That( page.IsFirstNameRequiredMsgShown(), Is.True );
            Assert.That( page.IsEmailRequiredMsgShown(), Is.True);
            Assert.That(page.IsRequiredPhoneMsgShown(), Is.True);
        }

        [When(@"I provide my comment and contact details")]
        public void WhenIProvideMyCommentAndContactDetails()
        {
            _pageFactory.Init<ContactUsPage>()
                .WithFullName()
                .WithEmail()
                .WithPhone()
                .WithComments();
        }

        [Then(@"I should see a human test validation message")]
        public void ThenIShouldSeeAHumanTestValidationMessage()
        {
            Assert.That(_pageFactory.Init<ContactUsPage>().IsHumanTestValidationMsgShown(), Is.True);
        }
    }
}