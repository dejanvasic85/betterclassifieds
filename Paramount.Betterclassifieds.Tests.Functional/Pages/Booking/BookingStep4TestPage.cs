using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step4.aspx")]
    [TestPage(RelativeUrl = "BundleBooking/BundlePage4.aspx")]
    public class BookingStep4TestPage : BookingTestPage
    {
        public BookingStep4TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Private Elements

        private IWebElement EditionsElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlUpcomingEditions")); }
        }

        private SelectElement InsertionsElement
        {
            get { return new SelectElement(FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlInserts"))); }
        }

        #endregion

        public void SelectOnlineStartDate(DateTime date)
        {
            var linkToClick = WebDriver
                .FindElements(By.CssSelector("#ctl00_ContentPlaceHolder1_calStartDate > tbody > tr > td > a[title]"))
                .FirstOrDefault(link => link.GetAttribute("title").EqualTo(date.ToString("MMMM dd")));

            if (linkToClick != null) linkToClick.ClickOnElement();
        }

        public void SelectFirstEditionDate()
        {
            EditionsElement.SelectOptionIndex(1);
        }

        public void SelectInsertionCount(int insertions)
        {
            InsertionsElement.SelectByIndex(insertions);
        }
    }
}