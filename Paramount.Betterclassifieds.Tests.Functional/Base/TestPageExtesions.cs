using System;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Base
{
    public static class TestPageExtesions
    {
        public static ITestPage InitialiseElements(this ITestPage testPage)
        {
            var typeofPage = testPage.GetType();

            var field = typeofPage
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .FirstOrDefault(f => f.FieldType == typeof(IWebDriver));

            if (field == null)
            {
                throw new Exception("Unable to obtain web driver from your page. Please ensure that you have a WebDriver as a field.");
            }

            var driver =  (IWebDriver) field.GetValue(testPage);

            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(driver, testPage);
            return testPage;
        }
    }
}