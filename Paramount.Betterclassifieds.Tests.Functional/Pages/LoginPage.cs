using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Account/Login")]
    public class LoginTestPage : BaseTestPage
    {
        public LoginTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        private IWebElement UsernameElement
        {
            get { return FindElement(By.Id("Username")); }
        }

        private IWebElement PasswordElement
        {
            get { return FindElement(By.Id("Password")); }
        }

        private IWebElement LoginButtonElement
        {
            get { return FindElement(By.Id("btnLogin")); }
        }

        #region Public Methods

        public void SetUsername(string username)
        {
            UsernameElement.SendKeys(username);
        }

        public void SetPassword(string password)
        {
            PasswordElement.SendKeys(password);
        }

        public void ClickLogin()
        {
            LoginButtonElement.Click();
        }

        #endregion
    }
}