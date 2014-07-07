using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Account/Login")]
    public class RegisterNewUserTestPage : BaseTestPage
    {
        public RegisterNewUserTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements
        private IWebElement EmailElement
        {
            get { return FindElement(By.Id("RegisterEmail")); }
        }

        private IWebElement EmailConfirmElement
        {
            get { return FindElement(By.Id("ConfirmEmail")); }
        }

        private IWebElement PasswordElement
        {
            get { return FindElement(By.Id("RegisterPassword")); }
        }

        private IWebElement PasswordConfirmElement
        {
            get { return FindElement(By.Id("ConfirmPassword")); }
        }

        private IWebElement FirstNameElement
        {
            get { return FindElement(By.Id("FirstName")); }
        }

        private IWebElement LastNameElement
        {
            get { return FindElement(By.Id("LastName")); }
        }
        
        private IWebElement PostCodeElement
        {
            get { return FindElement(By.Id("PostCode")); }
        }

        private IWebElement RegisterButton
        {
            get { return FindElement(By.Id("btnRegister")); }
        }
#endregion

        #region Public Methods

        public void SetPostcode(string postcode)
        {
            PostCodeElement.SendKeys(postcode);
        }
        
        public void SetLastName(string lastName)
        {
            LastNameElement.SendKeys(lastName);
        }

        public void SetFirstName(string firstName)
        {
            FirstNameElement.SendKeys(firstName);
        }

        public void SetEmail(string email)
        {
            EmailElement.SendKeys(email);
        }

        public void SetEmailConfirmation(string email)
        {
            EmailConfirmElement.SendKeys(email);
        }
        
        public void SetPassword(string password)
        {
            PasswordElement.SendKeys(password);
        }

        public void SetPasswordConfirmation(string password)
        {
            PasswordConfirmElement.SendKeys(password);
        }

        public void ClickRegister()
        {
            RegisterButton.Click();
        }

        #endregion
    }
}