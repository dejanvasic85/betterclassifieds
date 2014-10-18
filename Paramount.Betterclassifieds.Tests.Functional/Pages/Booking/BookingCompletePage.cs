using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Success", Title = "Booking Complete")]
    public class BookingCompletePage : TestPage
    {
        public BookingCompletePage(IWebDriver webdriver, IConfig config) : base(webdriver, config)
        {
        }
        
    }
}