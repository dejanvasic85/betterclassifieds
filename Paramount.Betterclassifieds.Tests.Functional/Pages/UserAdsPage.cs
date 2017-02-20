using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute("UserAds")]
    internal class UserAdsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public UserAdsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public UserAdsPage EditAd(int adId)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector($"[data-adid='{adId}']")));
            element.ClickOnElement();
            return this;
        }

        public UserAdsPage EditFirstAdWithTitle(string title)
        {
            GetAdElements()
                .First(el => el.FindElement(By.ClassName("media-heading")).Text == title)
                .FindElement(By.CssSelector("[data-adid]"))
                .ClickOnElement();

            return this;
        }

        public IEnumerable<AdTestData> GetAvailableAdsForCurrentUser()
        {
            return GetAdElements()
                .Select(adElement => new AdTestData
                {
                    AdId = int.Parse(adElement.FindElement(By.CssSelector("[data-adid]")).GetAttribute("data-adid")),
                    Title = adElement.FindElement(By.ClassName("media-heading")).Text
                });
        }

        private IEnumerable<IWebElement> GetAdElements()
        {
            return _webDriver.FindElements(By.ClassName("listing-results"));
        }

    }
}
