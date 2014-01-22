using System.Threading;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step2.aspx")]
    public class OnlineBookingStep2TestPage : OnlineBookingBaseTestPage
    {
        public OnlineBookingStep2TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        private IWebElement ParentCategoryElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMainCategory")); }
        }

        private IWebElement SubCategoryElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubCategory")); }
        }
        
        public void SelectParentCategory(string categoryName)
        {
            WebDriver.SelectOption(ParentCategoryElement, categoryName);
        }

        public void SelectSubCategory(string categoryName)
        {
            // For life of me i cannot work out the stale element reference at this point
            // So just fkn sleep it ( just for now )
            Thread.Sleep(2000); 

            WebDriver.SelectOption(SubCategoryElement, categoryName);
        }
    }
}