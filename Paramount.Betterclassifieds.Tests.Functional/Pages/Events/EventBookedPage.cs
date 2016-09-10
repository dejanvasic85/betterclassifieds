using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("Event/EventBooked")]
    internal class EventBookedPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EventBookedPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "eventBookedSuccessMsg")]
        public IWebElement SuccessTextElement { get; set; }

        public string GetSuccessText()
        {
            return SuccessTextElement.Text;
        }
            
        public EventBookedPage AssignGroup(string groupName, string guestFullName)
        {
            var groupDropDown = _webDriver.FindElement(By.CssSelector("[data-guestName='" + guestFullName + "']"));
            groupDropDown.SelectOption(groupName);
            var parentForm = _webDriver.ExecuteJavaScript<IWebElement>("return $(arguments[0]).closest('.form-group-lg').get(0)", groupDropDown);

            _webDriver.WaitForJqueryAjax();
            _webDriver.WaitFor(driver => parentForm.HasClass("has-success"), secondsToWait: 5);
            
            return this;
        }
    }
}