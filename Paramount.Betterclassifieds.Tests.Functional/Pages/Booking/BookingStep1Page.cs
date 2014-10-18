using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step/1")]
    public class BookingStep1Page : BookingTestPage
    {
        public BookingStep1Page(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements

        [FindsBy(How = How.Id, Using = "parentCategoryId"), UsedImplicitly]
        private IWebElement ParentCategoryElement;

        [FindsBy(How =  How.Id, Using = "subCategoryId"), UsedImplicitly]
        private IWebElement SubCategoryElement;

        #endregion
        
        public BookingStep1Page WithParentCategory(string categoryName)
        {
            ParentCategoryElement.SelectOption(categoryName);
            WaitForAjax();
            return this;
        }

        public BookingStep1Page WithSubCategory(string categoryName)
        {
            SubCategoryElement.SelectOption(categoryName);
            WaitForAjax();
            return this;
        }
    }
}