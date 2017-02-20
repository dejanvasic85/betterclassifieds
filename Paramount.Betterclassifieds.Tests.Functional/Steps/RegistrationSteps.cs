using System;
using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.ContextData;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class RegistrationSteps
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
            _dataRepository.DropCreateUser(username, password, email, RoleType.Advertiser);

            _userContext.Username = username;
            _userContext.Password = password;
            _userContext.Email = email;
        }

        [Given(@"a registered user ""(.*)"" with password ""(.*)"" and email ""(.*)"" exists")]
        public void GivenARegisteredUserWithPasswordAndEmailExists(string username, string password, string email)
        {
            GivenIAmARegisteredUserWithUsernameAndPassword(username, password, email);
        }


        [Given(@"The user with username ""(.*)"" and email ""(.*)"" does not exist")]
        public void GivenTheUserWithUsernameDoesNotExist(string username, string email)
        {
            _dataRepository.DropUserIfExists(username, email);
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
            registrationPage.SetPhone(telephone);
        }

        [Given(@"I have entered my account details ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyAccountDetails(string email, string password)
        {
            var registrationPage = _pageBrowser.Init<RegisterNewUserTestPage>();
            registrationPage.SetPassword(password);
            //registrationPage.SetPasswordConfirmation(password);
            registrationPage.SetEmail(email);
            //registrationPage.SetEmailConfirmation(email);
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


    }
}
