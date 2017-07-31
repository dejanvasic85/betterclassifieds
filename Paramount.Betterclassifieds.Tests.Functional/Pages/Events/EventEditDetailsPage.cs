using System.Security.Policy;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("editAd/eventDetails/{0}")]
    public class EventEditDetailsPage : ITestPage
    {
        private readonly IWebDriver _driver;

        public EventEditDetailsPage(IWebDriver driver)
        {
            _driver = driver;
        }

        [FindsBy(How = How.Id, Using = "Title")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.Id, Using = "Description")]
        public IWebElement Description { get; set; }

        [FindsBy(How = How.Id, Using = "save-resend-tickets")]
        public IWebElement SaveAndResendNotification { get; set; }

        public EventEditDetailsPage WithTitle(string title)
        {
            Title.FillText(title);
            return this;
        }

        public EventEditDetailsPage WithDescription(string description)
        {
            Description.FillText(description);
            return this;
        }

        public EventEditDetailsPage SaveAndSendNotifications()
        {
            _driver.JsClick(SaveAndResendNotification);
            _driver.GetToastSuccessMsgWhenVisible();
            return this;
        }
    }
}