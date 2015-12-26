using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    /// <summary>
    /// Base page that contains the elements to the navigation bar and login/place ad buttons
    /// </summary>
    [NavRoute(RelativeUrl = "")]
    internal class ApplicationPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public ApplicationPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.LinkText, Using = "Place new Ad"), UsedImplicitly]
        private IWebElement PlaceNewAdButton;

        [FindsBy(How = How.LinkText, Using = "Login / Register"), UsedImplicitly]
        private IWebElement LoginOrRegisterButton;

        public ApplicationPage PlaceNewAd()
        {
            this.PlaceNewAdButton.ClickOnElement();
            return this;
        }

        public string GetUrl()
        {
            return _webDriver.Url;
        }
    }
}
