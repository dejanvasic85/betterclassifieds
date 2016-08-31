using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("EditAd/remove-guest-complete")]
    internal class RemoveGuestCompletePage : EventDashboardSubPage, ITestPage
    {
        private readonly IWebDriver _webDriver;

        public RemoveGuestCompletePage(IWebDriver webDriver)
            : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetPageHeader()
        {
            return _webDriver.FindElement(By.ClassName("page-header")).Text;
        }

        public string GetSuccessMessage()
        {
            return _webDriver.FindElement(By.ClassName("alert")).Text;
        }
    }
}