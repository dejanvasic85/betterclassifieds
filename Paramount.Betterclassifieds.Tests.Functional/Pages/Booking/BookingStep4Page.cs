using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step/4")]
    public class BookingStep4Page : BookingTestPage
    {
        public BookingStep4Page(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements

        [FindsBy(How = How.Id, Using = "DetailsAreCorrect"), UsedImplicitly]
        private IWebElement DetailsAreCorrectCheckBox;

        [FindsBy(How = How.Id, Using = "AgreeToTerms"), UsedImplicitly]
        private IWebElement AgreeToTermsCheckBox;

        #endregion

        public BookingStep4Page AgreeToTermsAndConditions()
        {
            DetailsAreCorrectCheckBox.Click();
            AgreeToTermsCheckBox.Click();
            return this;
        }
    }
}