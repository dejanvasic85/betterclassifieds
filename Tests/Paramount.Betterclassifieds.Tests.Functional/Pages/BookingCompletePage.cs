using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Default.aspx", Title = "Booking Complete")]
    public class BookingCompleteTestPage : BaseTestPage
    {
        public BookingCompleteTestPage(IWebDriver webdriver, IConfig config) : base(webdriver, config)
        {
        }

        
    }
}