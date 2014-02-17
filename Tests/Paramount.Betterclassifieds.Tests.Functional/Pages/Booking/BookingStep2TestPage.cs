using System.Threading;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step2.aspx")]
    [TestPage(RelativeUrl = "BundleBooking/BundlePage2.aspx")]
    public class BookingStep2TestPage : BookingBaseTestPage
    {
        public BookingStep2TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements

        private IWebElement ParentCategoryElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlMainCategory")); }
        }

        private IWebElement SubCategoryElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ddlSubCategory")); }
        }

        private IWebElement SeleniumPublicationElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_ucxPaperList_grdPapers_ctl02_chkPaper")); }
        }

        #endregion


        public void SelectParentCategory(string categoryName)
        {
            ParentCategoryElement.SelectOption(categoryName);
        }

        public void SelectSubCategory(string categoryName)
        {
            SubCategoryElement.SelectOption(categoryName);
        }

        public void SelectSeleniumPublication()
        {
            // Find the publication element by class
            SeleniumPublicationElement.Click();
        }
    }
}