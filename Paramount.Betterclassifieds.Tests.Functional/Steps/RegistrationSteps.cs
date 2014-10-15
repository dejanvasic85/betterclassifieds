using System;
using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class RegistrationSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _dataRepository;
        private readonly RegistrationContext _registrationContext;

        public RegistrationSteps(PageBrowser pageBrowser, ITestDataRepository dataRepository, RegistrationContext registrationContext)
        {
            _pageBrowser = pageBrowser;
            _dataRepository = dataRepository;
            _registrationContext = registrationContext;
        }

        [Given(@"I am a registered user with username ""(.*)"" and password ""(.*)"" and email ""(.*)""")]
        public void GivenIAmARegisteredUserWithUsernameAndPassword(string username, string password, string email)
        {
            _dataRepository.AddUserIfNotExists(username, password, email, RoleType.Advertiser);
        }

        [Given(@"The user with username ""(.*)"" does not exist")]
        public void GivenTheUserWithUsernameDoesNotExist(string username)
        {
            _dataRepository.DropUserIfExists(username);
        }

        [Given(@"I navigate to the registration page")]
        public void GivenINavigateToTheRegistrationPage()
        {
            _pageBrowser.NavigateTo<RegisterNewUserTestPage>();
        }

        [Given(@"I have entered my personal details ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyPersonalDetails(string firstName, string lastName, string address, string suburb, string state, string postcode, string telephone)
        {
            var registrationPage = _pageBrowser.Init<RegisterNewUserTestPage>();
            registrationPage.SetFirstName(firstName);
            registrationPage.SetLastName(lastName);
            registrationPage.SetPostcode(postcode);
        }

        [Given(@"I have entered my account details ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyAccountDetails(string email, string password)
        {
            var registrationPage = _pageBrowser.Init<RegisterNewUserTestPage>();
            registrationPage.SetPassword(password);
            registrationPage.SetPasswordConfirmation(password);
            registrationPage.SetEmail(email);
            registrationPage.SetEmailConfirmation(email);
        }

        [When(@"I click register button")]
        public void WhenIClickRegisterButton()
        {
            // Store the time that this was clicked
            // See ThenARegistrationEmailShouldBeSentTo method where this is retrieved
            _registrationContext.StartRegistrationTime = DateTime.Now;

            var registrationPage = _pageBrowser.Init<RegisterNewUserTestPage>();
            registrationPage.ClickRegister();
        }

        [Then(@"the user ""(.*)"" should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully(string username)
        {
            // Assert
            Assert.IsTrue(_dataRepository.RegistrationExistsForEmail(username));
        }

        [Then(@"a registration email should be sent to ""(.*)""")]
        public void ThenARegistrationEmailShouldBeSentTo(string userEmail)
        {
            var emailsQueued = _dataRepository.GetSentEmailsFor(userEmail);

            Assert.IsTrue(emailsQueued.Any(e => 
                e.ModifiedDate >= _registrationContext.StartRegistrationTime &&
                e.DocType == "NewRegistration"));
        }

        [Then(@"I should see a thank you page with confirmation")]
        public void ThenIShouldSeeAThankYouPageWithConfirmation()
        {
            var registrationSuccess = _pageBrowser.Init<RegistrationSuccessPage>();
            Assert.That(registrationSuccess.GetConfirmationText(), Is.EqualTo("Please click on the confirmation link sent to your email and you will be able to start using your account!"));
            Assert.That(registrationSuccess.GetThankYouHeading(), Is.EqualTo("Thank you for signing up"));
        }
    }

    public class RegistrationContext
    {
        public DateTime StartRegistrationTime { get; set; }
    }
}
