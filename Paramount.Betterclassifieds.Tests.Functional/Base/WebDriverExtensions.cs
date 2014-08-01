﻿using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class WebDriverExtensions
    {
        public static void WaitForAjax(this IWebDriver webDriver, int timeOutSeconds = 30)
        {
            var startTime = DateTime.Now;

            while (true)
            {
                Thread.Sleep(1000); // Wait just a second for jQuery to load first

                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                var ajaxIsComplete = javaScriptExecutor != null && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");

                if (ajaxIsComplete) break;

                Thread.Sleep(500);

                if ((DateTime.Now - startTime).TotalSeconds > timeOutSeconds)
                {
                    throw new Exception("Time out waiting for ajax response");
                }
            }
        }

        public static void WaitForLoader(this IWebDriver webDriver, int timeOutSeconds = 30)
        {
            var waitForElement = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            waitForElement.Until(ExpectedConditions.ElementExists(By.ClassName("loader")));

            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            wait.Until(driver =>
            {
                var classAttr = driver.FindElement(By.ClassName("loader")).GetAttribute("class");
                return classAttr.Contains("hide");
            });
        }

    }
}