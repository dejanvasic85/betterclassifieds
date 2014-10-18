using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;
using System;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Booking/Step/3")]
    public class BookingStep3Page : BookingTestPage
    {
        public BookingStep3Page(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Private Elements

        [FindsBy(How = How.Id, Using = "StartDate"), UsedImplicitly]
        private IWebElement StartDateElement;

        #endregion

        public BookingStep3Page WithStartDate(DateTime date)
        {
            StartDateElement.FillText(date.ToString("dd/MM/yyyy"));
            return this;
        }
    }
}