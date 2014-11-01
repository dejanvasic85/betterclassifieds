using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Admin
{
    [TestPage(RelativeUrl = "Login.aspx")]
    public class AdminLoginPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public AdminLoginPage(IWebDriver webdriver, IConfig config)
        {
            _webdriver = webdriver;
        }

        [FindsBy(How = How.Id, Using = "ctl00_cphContentBody_LoginControl1_Login1_UserName")]
        private IWebElement Username { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_cphContentBody_LoginControl1_Login1_Password")]
        private IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "ctl00_cphContentBody_LoginControl1_Login1_LoginButton")]
        private IWebElement LoginButton { get; set; }

        public AdminLoginPage WithUsername(string username)
        {
            this.Username.FillText(username);
            return this;
        }

        public AdminLoginPage WithPassword(string password)
        {
            this.Password.FillText(password);
            return this;
        }

        public AdminLoginPage Login()
        {
            this.LoginButton.ClickOnElement();
            return this;
        }

    }
}
