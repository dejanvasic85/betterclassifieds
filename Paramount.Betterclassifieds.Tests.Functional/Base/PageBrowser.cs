using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public T Init<T>(params object[] query) where T : ITestPage
        {
            return Init<T>(true, query);
        }

        public T Init<T>(bool ensureUrl = true, params object[] query) where T : ITestPage
        {
            var page = Create<T>();
            if (ensureUrl)
            {
                // Some pages may by accessed by more than one URL
                var attr = typeof(T).GetCustomAttributes<NavRouteAttribute>();

                var acceptedUrls = attr
                    .Select(a => GetBaseUrl().ToLower() + string.Format(a.RelativeUrl.ToLower(), query))
                    .Concat(attr.Select(b => GetBaseUrl(true).ToLower() + string.Format(b.RelativeUrl.ToLower(), query)))
                    .ToList();

                Console.WriteLine("Init: Current URL " + WebDriver.Url);
                foreach (var acceptedUrl in acceptedUrls)
                {
                    Console.WriteLine("Accepted: " + acceptedUrl);
                }



                // Let's wait until the page is loaded before we initialise the elements
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(60));
                wait.Until(drv => acceptedUrls.Any(drv.Url.ToLower().Contains));
            }

            page.InitialiseElements();
            return page;
        }

        public T Create<T>() where T : ITestPage
        {
            // So far all our pages have only two types of constructors WebDriver or WebDriver,Config
            var objectsToPassToCtor = new List<object> { WebDriver };
            var typeOfPage = typeof(T);
            var ctorParams = typeOfPage.GetConstructors()[0].GetParameters();
            if (ctorParams.Length > 1)
                objectsToPassToCtor.Add(Config);

            return (T)Activator.CreateInstance(typeOfPage, objectsToPassToCtor.ToArray());
        }

        public TPage GoTo<TPage>(params object[] query) where TPage : ITestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<NavRouteAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            WebDriver.Navigate().GoToUrl(fullPageUrl);
            return Init<TPage>(query: query);
        }

        public void NavigateTo<TPage>(params object[] query) where TPage : ITestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<NavRouteAttribute>().RelativeUrl;
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

        /// <summary>
        /// Runs the required action until it returns true
        /// </summary>
        public bool WaitUntil(Func<bool> func, int maxSecondsToWait = 5)
        {
            try
            {
                var wait = new WebDriverWait(this.WebDriver, TimeSpan.FromSeconds(maxSecondsToWait));
                wait.Until(drv => func);
                return true; // If we got here then we didn't time out!
            }
            catch (WebDriverTimeoutException)
            {
                // Boom - the method kept returning false so return false!
                return false;
            }
        }
    }
}