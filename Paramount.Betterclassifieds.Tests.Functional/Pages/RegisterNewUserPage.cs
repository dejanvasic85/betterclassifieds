using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Account/Login")]
    public class RegisterNewUserTestPage : TestPage
    {
        public RegisterNewUserTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements


        [FindsBy(How = How.Id, Using = "RegisterEmail"), UsedImplicitly]
        private IWebElement EmailElement;

        [FindsBy(How = How.Id, Using = "ConfirmEmail"), UsedImplicitly]
        private IWebElement EmailConfirmElement;

        [FindsBy(How = How.Id, Using = "RegisterPassword"), UsedImplicitly]
        private IWebElement PasswordElement;

        [FindsBy(How = How.Id, Using = "ConfirmPassword"), UsedImplicitly]
        private IWebElement PasswordConfirmElement;

        [FindsBy(How = How.Id, Using = "FirstName"), UsedImplicitly]
        private IWebElement FirstNameElement;

        [FindsBy(How = How.Id, Using = "LastName"), UsedImplicitly]
        private IWebElement LastNameElement;

        [FindsBy(How = How.Id, Using = "PostCode"), UsedImplicitly]
        private IWebElement PostCodeElement;

        [FindsBy(How = How.Id, Using = "btnRegister"), UsedImplicitly]
        private IWebElement RegisterButton;

        #endregion

        #region Driving Methods

        public void SetPostcode(string postcode)
        {
            PostCodeElement.FillText(postcode);
        }

        public void SetLastName(string lastName)
        {
            LastNameElement.FillText(lastName);
        }

        public void SetFirstName(string firstName)
        {
            FirstNameElement.FillText(firstName);
        }

        public void SetEmail(string email)
        {
            EmailElement.FillText(email);
        }

        public void SetEmailConfirmation(string email)
        {
            EmailConfirmElement.FillText(email);
        }

        public void SetPassword(string password)
        {
            PasswordElement.FillText(password);
        }

        public void SetPasswordConfirmation(string password)
        {
            PasswordConfirmElement.FillText(password);
        }

        public void ClickRegister()
        {
            RegisterButton.Click();
        }

        #endregion
    }
}