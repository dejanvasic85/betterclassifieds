using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Register.aspx")]
    public class RegisterNewUserPage : BasePage
    {
        public RegisterNewUserPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        public void SetPersonalDetails(string firstName, string lastName, string address, string suburb, string state, string postcode, string telephone)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetAddress(address);
            SetSuburb(suburb);
            SetState(state);
            SetPostcode(postcode);
            SetTelephone(telephone);
        }

        private void SetTelephone(string telephone)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_PhoneNumberTextBox")).SendKeys(telephone);
        }

        private void SetPostcode(string postcode)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_ZipCodeTextBox")).SendKeys(postcode);
        }

        private void SetState(string state)
        {
            var stateSelection = new SelectElement(FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_StateDropDownList")));
            stateSelection.SelectByValue(state);
        }

        private void SetSuburb(string suburb)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CityTextBox")).SendKeys(suburb);
        }

        private void SetAddress(string address)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_Address1TextBox")).SendKeys(address);
        }

        private void SetLastName(string lastName)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_LastNameTextBox")).SendKeys(lastName);
        }

        private void SetFirstName(string firstName)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_FirstNameTextBox")).SendKeys(firstName);
        }

        public void ClickNextOnPersonalDetailsView()
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_StartNavigationTemplateContainerID_StartNextButton")).Click();
        }

        public void SetUsername(string username)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_UserName")).SendKeys(username);
        }
        
        public void SetPassword(string password)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_Password")).SendKeys(password);
        }

        public void SetEmail(string email)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_Email")).SendKeys(email);
        }

        public void SetEmailConfirmation(string email)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_txtConfirmEmail")).SendKeys(email);
        }

        public void SetPasswordConfirmation(string password)
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_ConfirmPassword")).SendKeys(password);
        }

        public void ClickCheckAvailability()
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_btnCheckUsername")).Click();
        }

        public string GetUsernameAvailabilityMessage()
        {
            var element = FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_lblUsernameAvailability"));            
            return element.Text;
        }

        public void ClickCreateUser()
        {
            FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard___CustomNav1_StepNextButtonButton")).Click();
        }

        public string GetRegistrationCompletedMessage()
        {
            return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CompleteStepContainer_Label17")).Text;
        }
    }
}