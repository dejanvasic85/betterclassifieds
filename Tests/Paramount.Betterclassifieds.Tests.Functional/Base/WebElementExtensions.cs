using System;
using System.Linq;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class WebElementExtensions
    {
        public static void ClickOnElement(this IWebElement element)
        {
            int attempts = 0;
            do
            {
                try
                {
                    element.Click();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    if (attempts == 3)
                    {
                        break;
                    }
                    attempts++;
                }
            } while (true);
        }

        public static void SelectOption(this IWebElement webElement, string optionValue)
        {
            if (webElement == null)
            {
                throw new Exception("Web Element not found");
            }

            var option = webElement
                .FindElements(By.TagName("option"))
                .FirstOrDefault(o => StringExtensions.EqualTo(o.Text, optionValue, StringComparison.OrdinalIgnoreCase));

            if (option == null)
            {
                throw new Exception("Select Option " + optionValue + " not found");
            }

            webElement.ClickOnElement();
        }
    }
}