using System.Linq;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class OnlineBookingBaseTestPage : BaseTestPage
    {
        protected OnlineBookingBaseTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        protected IWebElement NextButtonElement
        {
            get
            {
                // Next button is different for bundle and online only unfortunately
                var regularBooking = WebDriver.FindElements(By.Id("ctl00_ContentPlaceHolder1_ucxNavButtons_btnNext")).FirstOrDefault();
                // If not null, then it's regular booking so return
                if (regularBooking != null)
                    return regularBooking;
                // Otherwise get the bundle booking next button
                return FindElement(By.Id("ctl00_ContentPlaceHolder1_btnNext"));
            }
        }

        public void Proceed()
        {
            NextButtonElement.Click();
        }
    }
}