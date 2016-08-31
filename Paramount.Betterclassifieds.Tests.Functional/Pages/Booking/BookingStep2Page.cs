using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Properties;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Booking
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
            _webdriver.ExecuteJavaScript("CKEDITOR.instances['OnlineAdDescription'].setData('" + description + "')");
            return this;
        }

        public BookingStep2Page WithStartDate(DateTime date)
        {
            StartDateElement.FillText(date.ToString("dd/MM/yyyy"));
            return this;
        }
    }
}