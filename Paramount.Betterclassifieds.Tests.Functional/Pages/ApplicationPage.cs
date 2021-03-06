﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Properties;

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

        [FindsBy(How = How.Id, Using = "lnkPlaceNewAd"), UsedImplicitly]
        private IWebElement PlaceNewAdButton;

        [FindsBy(How = How.Id, Using = "lnkRegisterOrLogin"), UsedImplicitly]
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
