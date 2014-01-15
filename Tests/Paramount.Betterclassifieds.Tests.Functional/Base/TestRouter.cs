using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class PageNavigator
    {
        private readonly IWebDriver _webDriver;
        private readonly IConfig _config;
        private readonly IPageFactory _pageFactory;

        public PageNavigator(IWebDriver webDriver, IConfig config, IPageFactory pageFactory)
        {
            _webDriver = webDriver;
            _config = config;
            _pageFactory = pageFactory;
        }

        public TPage NavigateTo<TPage>(IPageFactory pageFactory, params object[] query) where TPage : BasePage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageUrlAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            _webDriver.Navigate().GoToUrl(fullPageUrl);

            var page = pageFactory.Resolve<TPage>();
            return page;
        }

        public void NavigateTo<TPage>(params object[] query) where TPage : BasePage
        {
            var relativeUrl = typeof (TPage).GetCustomAttribute<TestPageUrlAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            _webDriver.Navigate().GoToUrl(fullPageUrl);
        }

        public void NavigateTo(string relativePath)
        {
            // Construct the path with base url
            var webPath = GetBaseUrl() + relativePath;
            
            // Go!
            _webDriver.Navigate().GoToUrl(webPath);
        }

        private string GetBaseUrl()
        {
            return _config.BaseUrl;
        }
    }
}