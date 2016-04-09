using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Paramount.Betterclassifieds.Tests.Functional.Features.Events
{
    [NavRoute("Event/EventBooked")]
    internal class EventBookedPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EventBookedPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "eventBookedSuccessMsg")]
        public IWebElement SuccessTextElement { get; set; }

        public string GetSuccessText()
        {
            return SuccessTextElement.Text;
        }
    }
}