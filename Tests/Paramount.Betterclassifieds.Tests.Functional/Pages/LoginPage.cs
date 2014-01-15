using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPageUrl(RelativeUrl = "Login.aspx")]
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver webdriver) : base(webdriver)
        {
        }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxLogin_Login1_UserName")]
        private IWebElement _usernameElement;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxLogin_Login1_Password")]
        private IWebElement _passwordElement;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxLogin_Login1_btnLogin")] 
        private IWebElement _loginButtonElement;

        public void SetUsername(string username)
        {
            _usernameElement.SendKeys(username);
        }

        public void SetPassword(string password)
        {
            _passwordElement.SendKeys(password);
        }

        public void ClickLogin()
        {
            _loginButtonElement.Click();
        }
    }
}