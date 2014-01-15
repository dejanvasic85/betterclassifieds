using System;
using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class RegistrationSteps
    {
        private readonly IPageFactory _pageFactory;
        private readonly ITestDataManager _dataManager;
        private readonly PageNavigator _pageNavigator;

        public RegistrationSteps(IPageFactory pageFactory, ITestDataManager dataManager, PageNavigator pageNavigator)
        {
            _pageFactory = pageFactory;
            _dataManager = dataManager;
            _pageNavigator = pageNavigator;
        }

        [Given(@"I am a registered user with username ""(.*)"" and password ""(.*)"" and email ""(.*)""")]
        public void GivenIAmARegisteredUserWithUsernameAndPassword(string username, string password, string email)
        {
            _dataManager.AddUserIfNotExists(username, password, email);
        }

        [Given(@"The user with username ""(.*)"" does not exist")]
        public void GivenTheUserWithUsernameDoesNotExist(string username)
        {
            _dataManager.DropUserIfExists(username);
        }
        
        [Given(@"I navigate to the registration page")]
        public void GivenINavigateToTheRegistrationPage()
        {
            _pageNavigator.NavigateTo<RegisterNewUserPage>();
        }

        [Given(@"I have entered my personal details ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyPersonalDetails(string firstName, string lastName, string address, string suburb, string state, string postcode, string telephone)
        {
            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            registrationPage.SetPersonalDetails(firstName, lastName, address, suburb, state,
                postcode, telephone);
        }

        [Given(@"I have entered my account details ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyAccountDetails(string username, string password, string email)
        {
            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            registrationPage.SetUsername(username);
            registrationPage.SetPassword(password);
            registrationPage.SetPasswordConfirmation(password);
            registrationPage.SetEmail(email);
            registrationPage.SetEmailConfirmation(email);
        }

        [Given(@"I click Next to proceed to account details")]
        public void GivenIClickNextToProceedToAccountDetails()
        {
            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            registrationPage.ClickNextOnPersonalDetailsView();
        }

        [Given(@"I click check availability button")]
        public void GivenIClickCheckAvailabilityButton()
        {
            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            registrationPage.ClickCheckAvailability();
        }

        [Then(@"user availability message should display ""(.*)""")]
        public void ThenUserAvailabilityMessageShouldDisplay(string expectedMessage)
        {
            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            var message = registrationPage.GetUsernameAvailabilityMessage();
            Assert.IsTrue(message.StartsWith(expectedMessage));
        }

        [When(@"I click Create User button")]
        public void WhenIClickCreateUserButton()
        {
            // Store the time that this was clicked
            // See ThenARegistrationEmailShouldBeSentTo method where this is retrieved
            ScenarioContext.Current.Add("StartRegistrationTime", DateTime.Now);

            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            registrationPage.ClickCreateUser();
        }

        [When(@"I navigate to the login page")]
        public void WhenINavigateToTheLoginPage()
        {
            _pageNavigator.NavigateTo("~/Login.aspx");
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithUsernameAndPassword(string p0, string p1)
        {
            WhenINavigateToTheLoginPage();

        }

        [Then(@"the user ""(.*)"" should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully(string username)
        {
            // Assert
            _dataManager.UserExists(username).IsTrue();
        }

        [Then(@"a registration email should be sent to ""(.*)""")]
        public void ThenARegistrationEmailShouldBeSentTo(string userEmail)
        {
            var registrationTime = ScenarioContext.Current.Get<DateTime>("StartRegistrationTime");
            var emailsQueued = _dataManager.GetSentEmailsFor(userEmail);

            Assert.IsTrue(emailsQueued.Any(e => e.CreateDateTime >= registrationTime && e.Subject == "New Registration"));
        }

        [Then(@"I should see registration message displayed ""(.*)""")]
        public void ThenIShouldSeeRegistrationMessageDisplayed(string expectedMessage)
        {
            var registrationPage = _pageFactory.Resolve<RegisterNewUserPage>();
            var currentMessage = registrationPage.GetRegistrationCompletedMessage();

            Assert.IsTrue(currentMessage.StartsWith(expectedMessage));
        }
    }
}
