using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step5.aspx")]
    public class OnlineBookingStep5TestPage : OnlineBookingBaseTestPage
    {
        public OnlineBookingStep5TestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        private IWebElement DetailsConfirmedElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_chkConfirm")); }
        }

        private IWebElement TermsAndConditionsElement
        {
            get { return FindElement(By.Id("ctl00_ContentPlaceHolder1_chkConditions")); }
        }
        
        public void AgreeToTermsAndConditions()
        {
            DetailsConfirmedElement.Click();
            TermsAndConditionsElement.Click();
        }
    }
}