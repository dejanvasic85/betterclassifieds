using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Ad/{0}/{1}")]
    public class OnlineAdTestPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public OnlineAdTestPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        #region Page Elements

        [FindsBy(How = How.Id, Using = "ContactName"), UsedImplicitly] private IWebElement ContactNameElement;

        [FindsBy(How = How.Id, Using = "ContactEmail"), UsedImplicitly] private IWebElement ContactEmailElement;

        [FindsBy(How = How.Id, Using = "ContactPhone"), UsedImplicitly] private IWebElement ContactPhoneElement;

        #endregion

        #region Driving Methods

        public string GetContactName()
        {
            return ContactNameElement.Text;
        }

        public string GetContactPhone()
        {
            return ContactPhoneElement.Text;
        }

        public string GetContactEmail()
        {
            return ContactEmailElement.Text;
        }

        #endregion

        public string GetTitle()
        {
            return _webdriver.Title;
        }

    }
}
