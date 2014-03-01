﻿using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class WebElementExtensions
    {
        public static void ClickOnElement(this IWebElement element)
        {
            RetryElementAction(() =>
            {
                try
                {
                    element.Click();
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        public static void SelectOption(this IWebElement webElement, string optionValue)
        {
            // Retry mechanism
            RetryElementAction(() =>
            {
                try
                {
                    var option = webElement
                        .FindElements(By.TagName("option"))
                        .FirstOrDefault(o => o.Text.EqualTo(optionValue));

                    if (option == null)
                        return false;

                    option.ClickOnElement();

                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        public static void SelectOptionIndex(this IWebElement webElement, int index)
        {
            // Retry mechanism
            RetryElementAction(() =>
            {
                try
                {
                    var element = new SelectElement(webElement);
                    element.SelectByIndex(index);
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        private static void RetryElementAction(Func<bool> action)
        {
            var attempts = 0;
            while (attempts < 5)
            {
                if (action())
                    break;
                Thread.Sleep(1000);
                attempts++;
            }
        }
    }
}