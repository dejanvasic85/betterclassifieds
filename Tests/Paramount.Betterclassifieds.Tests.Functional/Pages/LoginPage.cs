using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Login.aspx")]
    public class LoginTestPage : BaseTestPage
    {
        public LoginTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxLogin_Login1_UserName")]
        private readonly IWebElement _usernameElement = null;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxLogin_Login1_Password")]
        private readonly IWebElement _passwordElement = null;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxLogin_Login1_btnLogin")] 
        private readonly IWebElement _loginButtonElement = null;

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