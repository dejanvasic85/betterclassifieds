using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute("")]
    internal class HomeTestPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public HomeTestPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }
    }
}
