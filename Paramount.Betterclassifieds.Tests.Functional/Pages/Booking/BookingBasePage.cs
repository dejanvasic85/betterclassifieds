using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class BookingTestPage : TestPage
    {
        protected BookingTestPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        {
        }

        protected IWebElement NextButtonElement
        {
            get
            {
                return FindElement("ContentPlaceHolder1_btnNext",
                                   "ctl00_ContentPlaceHolder1_ucxNavButtons_btnNext",
                                   "ctl00_ContentPlaceHolder1_btnNext");
            }
        }

        public void Proceed()
        {

            WebDriver.WaitForAjax(); // This might not actually have JQuery.. so just sleep for a second
            Thread.Sleep(2000); // It slows down just a little bit.
            NextButtonElement.ClickOnElement();
        }
    }
}