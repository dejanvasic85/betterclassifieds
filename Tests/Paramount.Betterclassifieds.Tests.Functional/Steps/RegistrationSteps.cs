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
        private readonly Mocks.ITestDataManager dataManager;
        private readonly TestRouter testRouter;

        public RegistrationSteps(IWebDriver webDriver, IConfig configuration, Mocks.ITestDataManager dataManager, TestRouter testRouter)
            : base(webDriver, configuration)
        {
            this.dataManager = dataManager;
            this.testRouter = testRouter;
        }

        [Given(@"I navigate to the registration page")]
        public void GivenINavigateToTheRegistrationPage()
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            testRouter.NavigateTo(registrationPage);
        }

        [Given(@"I have entered my personal details ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyPersonalDetails(string firstName, string lastName, string address, string suburb, string state, string postcode, string telephone)
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            registrationPage.SetPersonalDetails(firstName, lastName, address, suburb, state,
                postcode, telephone);
        }

        [Given(@"I have entered my account details ""(.*)"", ""(.*)"", ""(.*)""")]
        public void GivenIHaveEnteredMyAccountDetails(string username, string password, string email)
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            registrationPage.SetUsername(username);
            registrationPage.SetPassword(password);
            registrationPage.SetPasswordConfirmation(password);
            registrationPage.SetEmail(email);
            registrationPage.SetEmailConfirmation(email);
        }

        [Given(@"I click Next to proceed to account details")]
        public void GivenIClickNextToProceedToAccountDetails()
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            registrationPage.ClickNextOnPersonalDetailsView();
        }

        [Given(@"I click check availability button")]
        public void GivenIClickCheckAvailabilityButton()
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            registrationPage.ClickCheckAvailability();
        }

        [Then(@"user availability message should display ""(.*)""")]
        public void ThenUserAvailabilityMessageShouldDisplay(string expectedMessage)
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            var message = registrationPage.GetUsernameAvailabilityMessage();
            Assert.IsTrue(message.StartsWith(expectedMessage));
        }

        [When(@"I click Create User button")]
        public void WhenIClickCreateUserButton()
        {
            // Store the time that this was clicked
            // See ThenARegistrationEmailShouldBeSentTo method where this is retrieved
            ScenarioContext.Current.Add("StartRegistrationTime", DateTime.Now);

            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            registrationPage.ClickCreateUser();
        }

        [Then(@"the user ""(.*)"" should be created successfully")]
        public void ThenTheUserShouldBeCreatedSuccessfully(string username)
        {
            // Assert
            dataManager.UserExists(username).IsTrue();
        }

        [Then(@"a registration email should be sent to ""(.*)""")]
        public void ThenARegistrationEmailShouldBeSentTo(string userEmail)
        {
            var registrationTime = ScenarioContext.Current.Get<DateTime>("StartRegistrationTime");
            var emailsQueued = dataManager.GetSentEmailsFor(userEmail);

            Assert.IsTrue(emailsQueued.Any(e => e.CreateDateTime >= registrationTime && e.Subject == "New Registration"));
        }

        [Then(@"I should see registration message displayed ""(.*)""")]
        public void ThenIShouldSeeRegistrationMessageDisplayed(string expectedMessage)
        {
            var registrationPage = Resolve<Pages.RegisterNewUserPage>();
            var currentMessage = registrationPage.GetRegistrationCompletedMessage();

            Assert.IsTrue(currentMessage.StartsWith(expectedMessage));
        }
    }
}
