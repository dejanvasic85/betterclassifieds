using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Booking/Step/1")]
    public class BookingStep1Page : BookingTestPage
    {
        #region Page Elements

        [FindsBy(How = How.Id, Using = "parentCategoryId"), UsedImplicitly]
        private IWebElement ParentCategoryElement;

        [FindsBy(How = How.Id, Using = "subCategoryId"), UsedImplicitly]
        private IWebElement SubCategoryElement;

        #endregion

        public BookingStep1Page(IWebDriver webdriver) : base(webdriver)
        {
        }

        public BookingStep1Page WithParentCategory(string categoryName)
        {
            ParentCategoryElement.SelectOption(categoryName);
            _webdriver.WaitForJqueryAjax();
            this.InitialiseElements();
            return this;
        }

        public BookingStep1Page WithSubCategory(string categoryName)
        {
            SubCategoryElement.SelectOption(categoryName);
            _webdriver.WaitForJqueryAjax();
            this.InitialiseElements();
            return this;
        }

        public override void Proceed()
        {
            _webdriver.WaitForJqueryAjax(10);
            base.Proceed();
        }
    }
}