using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Booking
{
    public abstract class BookingTestPage : ITestPage
    {
        protected readonly IWebDriver _webdriver;

        protected BookingTestPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        public virtual void Proceed()
        {
            _webdriver.FindElement(By.Id("btnSubmit")).ClickOnElement();
        }
    }
}