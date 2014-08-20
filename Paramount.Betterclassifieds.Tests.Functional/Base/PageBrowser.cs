using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class PageBrowser
    {
        protected readonly IWebDriver _webDriver;
        protected readonly IConfig _config;

        public PageBrowser(){ }

        public PageBrowser(IWebDriver webDriver, IConfig config)
        {
            _webDriver = webDriver;
            _config = config;
        }

        public T Init<T>(bool ensureUrl = true) where T : TestPage
        {
            var pageType = typeof(T);
            var page = (T)Activator.CreateInstance(pageType, _webDriver, _config);

            if (ensureUrl)
            {
                // Some pages may by accessed by more than one URL
                var attr = pageType.GetCustomAttributes<TestPageAttribute>();

                var acceptedUrls = attr.Select(a => GetBaseUrl() + a.RelativeUrl)
                    .Concat(attr.Select(b => GetBaseUrl(secure:true) + b.RelativeUrl));

                var webDriverUrl = _webDriver.Url.Split('?')[0];

                // Let's wait until the page is loaded before we initialise the elements
                var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
                wait.Until(driver => acceptedUrls.Any(url => url.Equals(webDriverUrl, StringComparison.OrdinalIgnoreCase)));
            }

            page.InitElements();
            return page;
        }

        public TPage GoTo<TPage>(params object[] query) where TPage : TestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            _webDriver.Navigate().GoToUrl(fullPageUrl);
            _webDriver.WaitForAjax();
            return Init<TPage>();
        }

        public void NavigateTo<TPage>(params object[] query) where TPage : TestPage
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

        public string GetAbsoluteUrl(string relativePath)
        {
            return GetBaseUrl() + relativePath;
        }

        public string GetBaseUrl(bool secure = false)
        {
            var url = GetConfiguredUrl();

            if (!url.Contains("https") && secure)
                return url.Replace("http", "https");

            return url;
        }

        public virtual string GetConfiguredUrl()
        {
            return _config.BaseUrl;
        }
    }

    public class AdminPageBrowser : PageBrowser
    {
        public AdminPageBrowser(IWebDriver webDriver, IConfig config)
            : base(webDriver, config)
        { }

        public override string GetConfiguredUrl()
        {
            return _config.BaseAdminUrl;
        }
    }
}