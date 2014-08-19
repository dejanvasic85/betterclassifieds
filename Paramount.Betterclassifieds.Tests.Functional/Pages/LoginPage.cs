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

        #region Elements

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

        #endregion 

        #region Public Methods

        public LoginTestPage WithUsername(string username)
        {
            UsernameElement.FillText(username);
            return this;
        }

        public LoginTestPage WithPassword(string password)
        {
            PasswordElement.SendKeys(password);
            return this;
        }

        public void ClickLogin()
        {
            LoginButtonElement.ClickOnElement();
        }

        #endregion
    }
}