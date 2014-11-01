using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class WebDriverExtensions
    {
        public static void WaitForJqueryAjax(this IWebDriver webDriver, int timeOutSeconds = 30)
        {
            WebDriverWait waitForJqueryToBeAvailable = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            waitForJqueryToBeAvailable.Until(drv => IsJqueryDefined((IJavaScriptExecutor)drv));

            WebDriverWait waitForJqueryAjaxToFinish = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeOutSeconds));
            waitForJqueryAjaxToFinish.Until(drv => IsJqueryAjaxComplete((IJavaScriptExecutor)drv));
        }

        public static bool IsJqueryDefined(this IJavaScriptExecutor executor)
        {
            return (bool)executor.ExecuteScript("return jQuery !== undefined");
        }

        public static bool IsJqueryAjaxComplete(this IJavaScriptExecutor executor)
        {
            return (bool) executor.ExecuteScript("return jQuery.active === 0");
        }
    }
}