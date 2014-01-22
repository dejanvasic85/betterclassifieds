using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        public T Init<T>() where T : BaseTestPage
        {
            Type pageType = typeof (T);
            var page = (T)Activator.CreateInstance(pageType, _webDriver, _config);
            var pageRelativeUrl = GetAbsoluteUrl(pageType.GetCustomAttribute<TestPageAttribute>().RelativeUrl);

            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(120));
            wait.Until(driver => driver.Url.StartsWith(pageRelativeUrl, StringComparison.OrdinalIgnoreCase));

            page.InitElements();
            return page;
        }

        public TPage NavigateToAndInit<TPage>(params object[] query) where TPage : BaseTestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            _webDriver.Navigate().GoToUrl(fullPageUrl);

            return Init<TPage>();
        }

        public void NavigateTo<TPage>(params object[] query) where TPage : BaseTestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            _webDriver.Navigate().GoToUrl(fullPageUrl);
        }

        public void NavigateTo(string relativePath)
        {
            // Construct the path with base url
            var webPath = GetAbsoluteUrl(relativePath);

            // Go!
            _webDriver.Navigate().GoToUrl(webPath);
        }

        private string GetAbsoluteUrl(string relativePath)
        {
            return GetBaseUrl() + relativePath;
        }

        private string GetBaseUrl()
        {
            return _config.BaseUrl;
        }
    }
}