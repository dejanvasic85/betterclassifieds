using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step/2")]
    public class BookingStep2Page : BookingTestPage
    {
        public BookingStep2Page(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Private Elements

        [FindsBy(How = How.Id, Using = "OnlineAdHeading"), UsedImplicitly]
        private IWebElement OnlineHeaderElement;

        [FindsBy(How = How.Id, Using = "OnlineAdDescription"), UsedImplicitly]
        private IWebElement OnlineAdDescriptionElement;

        #endregion

        public BookingStep2Page WithOnlineHeader(string adTitle)
        {
            OnlineHeaderElement.FillText(adTitle);
            return this;
        }

        public BookingStep2Page WithOnlineDescription(string description)
        {
            OnlineAdDescriptionElement.FillText(description);
            return this;
        }
    }
}