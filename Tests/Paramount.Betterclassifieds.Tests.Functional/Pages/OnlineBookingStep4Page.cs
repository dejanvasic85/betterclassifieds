﻿using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step4.aspx")]
    public class OnlineBookingStep4TestPage : OnlineBookingBaseTestPage
    {
        public OnlineBookingStep4TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }
        
        public void SelectOnlineStartDate(DateTime date)
        {
            var linkToClick = WebDriver
                .FindElements(By.CssSelector("#ctl00_ContentPlaceHolder1_calStartDate > tbody > tr > td > a[title]"))
                .FirstOrDefault(link => link.GetAttribute("title").EqualTo(date.ToString("MMMM dd")));

            if (linkToClick != null) linkToClick.Click();
        }
    }
}