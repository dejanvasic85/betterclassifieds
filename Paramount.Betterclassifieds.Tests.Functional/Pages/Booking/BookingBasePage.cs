using System.Linq;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public abstract class BookingBaseTestPage : BaseTestPage
    {
        protected BookingBaseTestPage(IWebDriver webdriver, IConfig config)
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
            NextButtonElement.ClickOnElement();
        }
    }
}