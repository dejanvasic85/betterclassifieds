using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Account/Login")]
    public class LoginTestPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public LoginTestPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        #region Elements

        [FindsBy(How = How.Id, Using = "Username"), UsedImplicitly]
        private IWebElement UsernameElement;
        
        [FindsBy(How =  How.Id, Using = "Password"), UsedImplicitly]
        private IWebElement PasswordElement;

        [FindsBy(How = How.Id, Using = "btnLogin"), UsedImplicitly]
        private IWebElement LoginButtonElement;

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

        public IWebDriver GetDriver()
        {
            return _webdriver;
        }
    }
}