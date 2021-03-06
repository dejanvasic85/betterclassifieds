﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
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
                
                // Let's wait until the page is loaded before we initialise the elements
                var wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(120));
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

        /// <summary>
        /// Navigates to a page and constructs the page object
        /// </summary>        
        /// <param name="query">If the page requires a parameter, then object is required to do key/value replacement.</param>
        public TPage GoTo<TPage>(params object[] query) where TPage : ITestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<NavRouteAttribute>().RelativeUrl;
            var fullPageUrl = GetBaseUrl() + string.Format(relativeUrl, query);
            WebDriver.Navigate().GoToUrl(fullPageUrl);
            return Init<TPage>(query: query);
        }

        /// <summary>
        /// Navigates to a particular page with an exact query string 
        /// </summary>
        public TPage GoTo<TPage>(string queryString) where TPage : ITestPage
        {
            var relativeUrl = typeof(TPage).GetCustomAttribute<NavRouteAttribute>().RelativeUrl;
            if (!queryString.StartsWith("?"))
            {
                queryString += "?";
            }
            var fullPageUrl = GetBaseUrl().Append(relativeUrl).TrimEnd('/').Append(queryString);

            WebDriver.Navigate().GoToUrl(fullPageUrl);

            return Init<TPage>();
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

        public string CurrentUrl()
        {
            return WebDriver.Url;
        }
    }
}