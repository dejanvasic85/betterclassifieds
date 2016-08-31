using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute("UserAds")]
    public class UserAdsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public UserAdsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public UserAdsPage EditAd(int adId)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(string.Format("[data-adid='{0}']", adId))));
            element.ClickOnElement();
            return this;
        }
    }
}
