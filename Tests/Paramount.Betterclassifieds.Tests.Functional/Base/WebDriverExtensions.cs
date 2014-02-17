using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class WebDriverExtensions
    {
        public static void WaitForAjax(this IWebDriver webDriver, int timeOutSeconds = 5)
        {
            var startTime = DateTime.Now;

            while (true)
            {
                var javaScriptExecutor = webDriver as IJavaScriptExecutor;
                var ajaxIsComplete = javaScriptExecutor != null && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");

                if (ajaxIsComplete) break;

                Thread.Sleep(100);

                if ((DateTime.Now - startTime).TotalSeconds > timeOutSeconds)
                {
                    throw new Exception("Time out waiting for ajax response");
                }
            }
        }

        public static void WaitForLoader(this IWebDriver webDriver, int timeOutSeconds = 30)
        {
            WebDriverWait waitForElement = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            waitForElement.Until(ExpectedConditions.ElementExists(By.ClassName("loader")));

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            wait.Until(driver =>
            {
                var classAttr = driver.FindElement(By.ClassName("loader")).GetAttribute("class");
                return classAttr.Contains("hide");
            });
        }

    }
}