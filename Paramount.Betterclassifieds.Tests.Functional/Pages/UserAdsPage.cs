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

        struct Locator
        {
            public static By EditAdButton = By.ClassName("btn-circle");
        }

        public UserAdsPage EditAd(int adId)
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector($"[data-adid='{adId}'] .btn-circle")));

            element.ClickOnElement();
            return this;
        }

        public UserAdsPage EditFirstAdWithTitle(string title)
        {
            GetAdElements()
                .First(el => el.FindElement(By.CssSelector("[data-title]")).Text == title)
                .FindElement(Locator.EditAdButton)
                .Click();

            return this;
        }

        public IEnumerable<AdTestData> GetAvailableAdsForCurrentUser()
        {
            return GetAdElements()
                .Select(adElement => new AdTestData
                {
                    AdId = int.Parse(adElement.FindElement(By.CssSelector("[data-adid]")).GetAttribute("data-adid")),
                    Title = adElement.FindElement(By.CssSelector("[data-title]")).Text
                });
        }

        private IEnumerable<IWebElement> GetAdElements()
        {
            return _webDriver.FindElements(By.ClassName("listings"));
        }

    }
}
