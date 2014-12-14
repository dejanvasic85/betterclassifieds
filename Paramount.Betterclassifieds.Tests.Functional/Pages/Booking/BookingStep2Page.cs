using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Booking/Step/2")]
    public class BookingStep2Page : BookingTestPage
    {
        public BookingStep2Page(IWebDriver webdriver)
            : base(webdriver)
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
            // The CLEditor is just weird. It uses a damn frame so we have to insert it via javascript
            // So we had to move the online editor to the _pg (paramount global) namespace
            // And call the cleditor directly (ughhh ugly)
            _webdriver.ExecuteJavaScript("_pg.onlineEditor[0].clear().execCommand('inserthtml', '" + description + "', null, null)");
            return this;
        }
    }
}