using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class PageBrowser
    {
        protected readonly IWebDriver WebDriver;
        protected readonly IConfig Config;

        public PageBrowser() { }

        public PageBrowser(IWebDriver webDriver, IConfig config)
        {
            WebDriver = webDriver;
            Config = config;
        }

        public T Init<T>(bool ensureUrl = true) where T : TestPage
        {
            WebDriver.WaitForJqueryAjax();

            var pageType = typeof(T);
            var page = (T)Activator.CreateInstance(pageType, WebDriver, Config);

            if (ensureUrl)
            {
                // Some pages may by accessed by more than one URL
                var attr = pageType.GetCustomAttributes<TestPageAttribute>();

                var acceptedUrls = attr
                    .Select(a => GetBaseUrl().ToLower() + a.RelativeUrl.ToLower())
                    .Concat(attr.Select(b => string.Format("{0}{1}", GetBaseUrl(secure: true), b.RelativeUrl).ToLower()));

                // Let's wait until the page is loaded before we initialise the elements
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
                wait.Until(drv => acceptedUrls.Any(drv.Url.ToLower().Contains));
            }

            page.InitElements();
            return page;
        }

        public TPage GoTo<TPage>(params object[] query) where TPage : TestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            WebDriver.Navigate().GoToUrl(fullPageUrl);
            return Init<TPage>();
        }

        public void NavigateTo<TPage>(params object[] query) where TPage : TestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<TestPageAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            WebDriver.Navigate().GoToUrl(fullPageUrl);
        }

        public void NavigateTo(string relativePath)
        {
            // Construct the path with base url
            var webPath = GetAbsoluteUrl(relativePath);

            // Go!
            WebDriver.Navigate().GoToUrl(webPath);
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
            return Config.BaseUrl;
        }

        public bool IsAspNotFoundDisplayed(int secondsToWait = 10)
        {
            var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(secondsToWait));
            return wait.Until(ExpectedConditions.TitleContains("The resource cannot be found"));
        }
    }
}