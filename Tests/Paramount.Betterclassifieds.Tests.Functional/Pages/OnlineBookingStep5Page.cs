using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step5.aspx")]
    public class OnlineBookingStep5Page : OnlineBookingBasePage
    {
        public OnlineBookingStep5Page(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_chkConfirm")]
        private readonly IWebElement _detailsConfirmedElement = null;

        [FindsBy(How = How.Id, Using = "ctl00_ContentPlaceHolder1_chkConditions")]
        private readonly IWebElement _termsAndConditionsElement = null;
        
        public void AgreeToTermsAndConditions()
        {
            _detailsConfirmedElement.Click();
            _termsAndConditionsElement.Click();
        }
    }
}