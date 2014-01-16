using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPageUrl(RelativeUrl = "Booking/Step2.aspx")]
    public class BookingStep2 : BookingBasePage
    {
        public BookingStep2(IWebDriver webdriver)
            : base(webdriver)
        { }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ddlMainCategory")]
        private readonly IWebElement _parentCategoryElement = null;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_ddlSubCategory")]
        private readonly IWebElement _subCategoryElement = null;
        
        public void SelectParentCategory(string categoryName)
        {
            SelectElement list = new SelectElement(_parentCategoryElement);
            list.SelectByText(categoryName);
        }

        public void SelectSubCategory(string categoryName)
        {
            SelectElement list = new SelectElement(_subCategoryElement);
            list.SelectByText(categoryName);
        }
    }
}