using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Booking/Step/4")]
    public class BookingStep4Page : BookingTestPage
    {
        public BookingStep4Page(IWebDriver webdriver)
            : base(webdriver)
        { }

        #region Page Elements

        [FindsBy(How = How.Id, Using = "DetailsAreCorrect"), UsedImplicitly]
        private IWebElement DetailsAreCorrectCheckBox;

        [FindsBy(How = How.Id, Using = "AgreeToTerms"), UsedImplicitly]
        private IWebElement AgreeToTermsCheckBox;

        [FindsBy(How = How.Id, Using = "BookingReference"), UsedImplicitly] 
        private IWebElement BookingReferenceSpan;

        #endregion

        public BookingStep4Page AgreeToTermsAndConditions()
        {
            DetailsAreCorrectCheckBox.Click();
            AgreeToTermsCheckBox.Click();
            return this;
        }

        public string GetBookingReference()
        {
            return BookingReferenceSpan.Text;
        }
    }
}