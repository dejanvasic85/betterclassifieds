using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public class RegisterNewUserPage : BasePage
    {
        public RegisterNewUserPage(IWebDriver webDriver)
            : base(webDriver)
        { }

        public override string RelativePath
        {
            get { return "Register.aspx"; }
        }

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
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_PhoneNumberTextBox")).SendKeys(telephone);
        }

        private void SetPostcode(string postcode)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_ZipCodeTextBox"))
                .SendKeys(postcode);
        }

        private void SetState(string state)
        {
            var stateSelection = new SelectElement(WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_StateDropDownList")));
            stateSelection.SelectByValue(state);
        }

        private void SetSuburb(string suburb)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CityTextBox"))
                .SendKeys(suburb);
        }

        private void SetAddress(string address)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_Address1TextBox"))
                .SendKeys(address);
        }

        private void SetLastName(string lastName)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_LastNameTextBox"))
                .SendKeys(lastName);
        }

        private void SetFirstName(string firstName)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_FirstNameTextBox"))
                .SendKeys(firstName);
        }

        public void ClickNextOnPersonalDetailsView()
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_StartNavigationTemplateContainerID_StartNextButton"))
                .Click();
        }

        public void SetUsername(string username)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_UserName"))
                .SendKeys(username);
        }
        
        public void SetPassword(string password)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_Password"))
                .SendKeys(password);
        }

        public void SetEmail(string email)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_Email"))
                .SendKeys(email);
        }

        public void SetEmailConfirmation(string email)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_ConfirmPassword"))
                .SendKeys(email);
        }

        public void SetPasswordConfirmation(string password)
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_txtConfirmEmail"))
                .SendKeys(password);
        }

        public void ClickCheckAvailability()
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_btnCheckUsername"))
                .Click();
        }

        public string GetUsernameAvailabilityMessage()
        {
            WebDriverWait wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_lblUsernameAvailability")));
            
            var element = WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard_CreateUserStepContainer_lblUsernameAvailability"));
            return element.Text;
        }

        public void ClickCreateUser()
        {
            WebDriver.FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxRegister_SiteCreateUserWizard___CustomNav1_StepNextButtonButton"))
                .Click();
        }
    }
}