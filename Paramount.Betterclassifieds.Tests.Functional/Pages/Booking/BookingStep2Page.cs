using System;
using OpenQA.Selenium;
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
        
        [FindsBy(How = How.Id, Using = "StartDate"), UsedImplicitly]
        private IWebElement StartDateElement;

        #endregion

        public BookingStep2Page WithOnlineHeader(string adTitle)
        {
            OnlineHeaderElement.FillText(adTitle);
            return this;
        }

        public BookingStep2Page WithOnlineDescription(string description)
        {
            // The CLEditor is just weird. It uses a damn frame so we have to insert it via javascript
            // So we had to move the online editor to the $paramount library
            // And call the cleditor directly (ughhh ugly)
            _webdriver.ExecuteJavaScript("$paramount.onlineEditor[0].clear().execCommand('inserthtml', '" + description + "', null, null)");
            return this;
        }

        public BookingStep2Page WithStartDate(DateTime date)
        {
            StartDateElement.FillText(date.ToString("dd/MM/yyyy"));
            return this;
        }
    }
}