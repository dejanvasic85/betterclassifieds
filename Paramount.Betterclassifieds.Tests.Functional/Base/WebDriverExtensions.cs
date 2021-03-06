﻿using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
{
    public static class WebDriverExtensions
    {


        public static void WaitForJqueryAjax(this IWebDriver webDriver, int timeOutSeconds = 30)
        {
            var waitForJqueryToBeAvailable = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            waitForJqueryToBeAvailable.Until(drv => IsJqueryDefined((IJavaScriptExecutor)drv));

            var waitForJqueryAjaxToFinish = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            waitForJqueryAjaxToFinish.Until(drv => IsJqueryAjaxComplete((IJavaScriptExecutor)drv));
        }

        public static void WaitFor(this IWebDriver webDriver, Func<IWebDriver, bool> condition, int secondsToWait = 30)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(secondsToWait));
            wait.Until(condition);
        }

        public static bool IsElementPresentBy(this IWebDriver webDriver, By by)
        {
            return webDriver.FindElements(by).Any();
        }

        public static bool IsJqueryDefined(this IJavaScriptExecutor executor)
        {
            return (bool)executor.ExecuteScript("return jQuery !== undefined;");
        }

        public static bool IsJqueryAjaxComplete(this IJavaScriptExecutor executor)
        {
            return (bool)executor.ExecuteScript("return jQuery.active === 0;");
        }

        public static void ExecuteJavaScript(this IWebDriver driver, string script, params object[] args)
        {
            driver.ToJavaScriptExecutor().ExecuteScript(script, args);
        }

        public static IWebElement JsClick(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(by));
            return driver.JsClick(element);
        }

        public static IWebElement JsClick(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript("arguments[0].focus();", element);
            Thread.Sleep(500);
            // Focus on the element before clicking otherwise knockout binding won't actually work
            driver.ExecuteJavaScript(" arguments[0].click()", element);
            return element;
        }

        public static IJavaScriptExecutor ToJavaScriptExecutor(this IWebDriver driver)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            if (javaScriptExecutor == null)
                throw new WebDriverException("Driver does not implement IJavaScriptExecutor");
            return javaScriptExecutor;
        }

        public static IWebElement ScrollElementToMiddle(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript("window.scrollTo(0, $(arguments[0]).offset().top - (window.innerHeight / 2));", element);
            Thread.Sleep(150);
            return element;
        }

        public static IWebElement WaitForToastrMessage(this IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            return wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("toast-success")));
        }

        public static string GetToastSuccessMsgWhenVisible(this IWebDriver driver)
        {
            return driver.WaitForToastrMessage().Text;
        }
        
    }
}