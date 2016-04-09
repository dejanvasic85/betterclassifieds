using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class WebElementExtensions
    {
        public static IWebElement ClickOnElement(this IWebElement element)
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

            return element;
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

        public static IWebElement FillText(this IWebElement webElement, string text)
        {
            webElement.Clear();
            webElement.SendKeys(text);
            return webElement;
        }

        public static bool HasAttributeValue(this IWebElement webElement, string attr, string value)
        {
            var webElementAttribute = webElement.GetAttribute(attr);

            if (webElementAttribute.IsNullOrEmpty())
                return false;

            return webElementAttribute.Equals(value, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetValue(this IWebElement webElement)
        {
            var valattr = webElement.GetAttribute("value");
            if (valattr == null)
            {
                return string.Empty;
            }

            return valattr;
        }

        public static bool HasClass(this IWebElement webElement, string className)
        {
            var classes = webElement.GetAttribute("class").Split(' ');
            return classes.Any(c => c.Equals(className, StringComparison.OrdinalIgnoreCase));
        }

        public static SelectElement ToSelectElement(this IWebElement webElement)
        {
            return new SelectElement(webElement);
        }

        public static SelectElement WithSelectedOptionValue(this SelectElement element, string optionValue)
        {
            element.SelectByValue(optionValue);
            return element;
        }
    }
}