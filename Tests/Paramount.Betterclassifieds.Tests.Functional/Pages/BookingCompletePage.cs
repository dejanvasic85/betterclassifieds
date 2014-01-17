using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Default.aspx", Title = "Booking Complete")]
    public class BookingCompletePage : BasePage
    {
        public BookingCompletePage(IWebDriver webdriver, IConfig config) : base(webdriver, config)
        {
        }

        
    }
}