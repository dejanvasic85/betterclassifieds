﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class OnlineBookingBaseTestPage : BaseTestPage
    {
        protected OnlineBookingBaseTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ucxNavButtons_btnNext")]
        private readonly IWebElement _nextButtonElement = null;

        public void Proceed()
        {
            _nextButtonElement.Click();
        }
    }
}