using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class RegistrationSteps : BaseStep
    {
        private readonly Mocks.ITestDataManager _dataManager;
        private readonly TestRouter _testRouter;

        public RegistrationSteps(IWebDriver webDriver, IConfig configuration, Mocks.ITestDataManager dataManager, TestRouter testRouter)
            : base(webDriver, configuration)
        {
            _dataManager = dataManager;
            _testRouter = testRouter;
        }

        [Given(@"I navigate to the registration page")]
        public void GivenINavigateToTheRegistrationPage()
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            _testRouter.NavigateTo(registrationPage);
        }
        
        [Given(@"I have entered my personal details ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyPersonalDetails(string firstName, string lastName, string address, string suburb, string state, string postcode, string telephone)
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            registrationPage.SetPersonalDetails(firstName, lastName, address, suburb, state,
                postcode, telephone);
        }

        [Given(@"I have entered my account details ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyAccountDetails(string username, string password, string email)
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            registrationPage.SetUsername(username);
            registrationPage.SetPassword(password);
            registrationPage.SetPasswordConfirmation(password);
            registrationPage.SetEmail(email);
            registrationPage.SetEmailConfirmation(email);
        }

        [Given(@"I click Next to proceed to account details")]
        public void GivenIClickNextToProceedToAccountDetails()
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            registrationPage.ClickNextOnPersonalDetailsView();
        }

        [Given(@"I click check availability button")]
        public void GivenIClickCheckAvailabilityButton()
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            registrationPage.ClickCheckAvailability();
        }

        [Then(@"user availability message should display ""(.*)""")]
        public void ThenUserAvailabilityMessageShouldDisplay(string expectedMessage)
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            var message = registrationPage.GetUsernameAvailabilityMessage();
            Assert.AreEqual(expectedMessage, message);
        }

        [When(@"I click Create User button")]
        public void WhenIClickCreateUserButton()
        {
            var registrationPage = new Pages.RegisterNewUserPage(WebDriver);
            registrationPage.ClickCreateUser();

            // Store the time that this was clicked
            // See ThenARegistrationEmailShouldBeSentTo method where this is retrieved
            ScenarioContext.Current.Add("UserRegisteredTime", DateTime.Now);
        }

        [Then(@"the user ""(.*)"" should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully(string username)
        {
            var userProfile = _dataManager.GetUserProfile(username);
            Assert.IsNotNull(userProfile);
            Assert.AreEqual(username, userProfile.Username);
        }

        [Then(@"a registration email should be sent to ""(.*)""")]
        public void ThenARegistrationEmailShouldBeSentTo(string userEmail)
        {
            var registrationTime = ScenarioContext.Current.Get<DateTime>("UserRegisteredTime");
            var emailsQueued = _dataManager.GetSentEmailsFor(userEmail);   
            
            Assert.IsTrue(emailsQueued.Any(e=>e.CreatedDateTime>= registrationTime && e.Subject == "New Registration"));
        }
    }
}
