using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPageUrl(RelativeUrl = "Booking/Step2.aspx")]
    public class OnlineBookingStep2Page : OnlineBookingBasePage
    {
        public OnlineBookingStep2Page(IWebDriver webdriver)
            : base(webdriver)
        { }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ddlMainCategory")]
        private readonly IWebElement _parentCategoryElement = null;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ddlSubCategory")]
        private readonly IWebElement _subCategoryElement = null;
        
        public void SelectParentCategory(string categoryName)
        {
            WebDriver.SelectOption(_parentCategoryElement, categoryName);
        }

        public void SelectSubCategory(string categoryName)
        {
            // For life of me i cannot work out the stale element reference at this point
            // So just fkn sleep it ( just for now )
            Thread.Sleep(2000); 

            WebDriver.SelectOption(_subCategoryElement, categoryName);
        }
    }
}