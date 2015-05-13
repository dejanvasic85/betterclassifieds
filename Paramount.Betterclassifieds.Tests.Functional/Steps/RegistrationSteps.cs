namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    using Mocks;
    using NUnit.Framework;
    using Pages;
    using System;
    using System.Linq;
    using TechTalk.SpecFlow;

    [Binding]
    public class RegistrationSteps
    {
        private readonly PageBrowser _pageBrowser;
        private readonly ITestDataRepository _dataRepository;
        private readonly UserContext _userContext;

        public RegistrationSteps(PageBrowser pageBrowser, ITestDataRepository dataRepository, UserContext userContext)
        {
            _pageBrowser = pageBrowser;
            _dataRepository = dataRepository;
            _userContext = userContext;
        }

        [Given(@"I am a registered user with username ""(.*)"" and password ""(.*)"" and email ""(.*)""")]
        public void GivenIAmARegisteredUserWithUsernameAndPassword(string username, string password, string email)
        {
            _dataRepository.AddUserIfNotExists(username, password, email, RoleType.Advertiser);

            _userContext.Username = username;
            _userContext.Password = password;
            _userContext.Email = email;
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
            _userContext.StartRegistrationTime = DateTime.Now;

            var registrationPage = _pageBrowser.Init<RegisterNewUserTestPage>();
            registrationPage.ClickRegister();
        }

        [Then(@"the user ""(.*)"" should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully(string username)
        {
            var registrationResult = _pageBrowser.WaitUntil(() => _dataRepository.RegistrationExistsForEmail(username));   
            
            // Assert
            Assert.That(registrationResult, Is.True);
        }

        [Then(@"a registration email should be sent to ""(.*)""")]
        public void ThenARegistrationEmailShouldBeSentTo(string userEmail)
        {
            var isRegistrationEmailQueued = _pageBrowser.WaitUntil(() =>
            {
                var emailsQueued = _dataRepository.GetSentEmailsFor(userEmail);

                return emailsQueued.Any(e =>
                    e.ModifiedDate >= _userContext.StartRegistrationTime &&
                    e.DocType == "NewRegistration");
            });

            Assert.That(isRegistrationEmailQueued, Is.True);
        }

        [Then(@"I should see a thank you page with confirmation")]
        public void ThenIShouldSeeAThankYouPageWithConfirmation()
        {
            var registrationSuccess = _pageBrowser.Init<RegistrationSuccessPage>();
            Assert.That(registrationSuccess.GetConfirmationText(), Is.EqualTo("Please click on the confirmation link sent to your email and you will be able to start using your account!"));
            Assert.That(registrationSuccess.GetThankYouHeading(), Is.EqualTo("Thank you for signing up"));
        }
    }
}
