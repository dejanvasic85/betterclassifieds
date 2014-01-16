using System;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class PageFactory
    {
        private readonly IWebDriver _webDriver;
        private readonly IConfig _config;

        public PageFactory(IWebDriver webDriver, IConfig config)
        {
            _webDriver = webDriver;
            _config = config;
        }

        public T Resolve<T>() where T : BasePage
        {
            var page = (T)Activator.CreateInstance(typeof(T), args: _webDriver);
            page.InitElements();
            return page;
        }

        public TPage NavigateToAndResolve<TPage>(params object[] query) where TPage : BasePage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageUrlAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            _webDriver.Navigate().GoToUrl(fullPageUrl);

            return Resolve<TPage>();
        }

        public void NavigateTo<TPage>(params object[] query) where TPage : BasePage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageUrlAttribute>().RelativeUrl;
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