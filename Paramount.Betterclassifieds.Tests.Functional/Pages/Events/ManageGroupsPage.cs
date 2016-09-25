using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Events
{
    [NavRoute("editad/manage-groups/{0}?eventId={1}")]
    internal class ManageGroupsPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public ManageGroupsPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "btnCreateGroup")]
        private IWebElement CreateGroupButton { get; set; }

        [FindsBy(How = How.Id, Using = "txtNewGroupName")]
        private IWebElement NewGroupNameInput { get; set; }

        [FindsBy(How = How.Id, Using = "txtNewGroupMaxGuests")]
        private IWebElement MaxGuestsInput { get; set; }

        [FindsBy(How = How.Id, Using = "btnSaveGroup")]
        private IWebElement SaveGroupButton { get; set; }

        [FindsBy(How = How.Id, Using = "btnLimitTickets")]
        public IWebElement LimitTicketsButton { get; set; }

        public By LimitTicketsViewLocator => By.Id("limitTickets");

        public ManageGroupsPage CreateGroup()
        {
            CreateGroupButton.Click();
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("createGroupForm")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("btnSaveGroup")));
            return this;
        }

        public ManageGroupsPage WithNewGroupName(string groupName)
        {
            NewGroupNameInput.SendKeys(groupName);
            return this;
        }

        public ManageGroupsPage WithMaxGuests(int maxGuests)
        {
            MaxGuestsInput.SendKeys(maxGuests.ToString());
            return this;
        }

        public ManageGroupsPage WithSelectedTickets(params string[] selectedTickets)
        {
            LimitTicketsButton.Click();
            
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(LimitTicketsViewLocator));
            
            var ticketCheckboxElements = _webDriver.FindElements(By.ClassName("ticket-group-option"));
            foreach (var ticketCheckboxElement in ticketCheckboxElements)
            {
                var attrValue = ticketCheckboxElement.GetAttribute("data-ticketName");
                if (selectedTickets.Any(st => st.Equals(attrValue, StringComparison.OrdinalIgnoreCase)))
                {
                    _webDriver.ScrollElementToMiddle(ticketCheckboxElement).Click();
                }
            }
            return this;
        }

        public ManageGroupsPage SaveGroup()
        {
            SaveGroupButton.Click();
            _webDriver.WaitForJqueryAjax();
            _webDriver.WaitFor(ExpectedConditions.InvisibilityOfElementLocated(By.Id("createGroupForm")));
            return this;
        }

        public IEnumerable<GroupViewInfo> GetCurrentGroups()
        {
            // Extract out all the current groups in the page
            return _webDriver.FindElements(By.ClassName("group-info"))
                .Select(parentElement => new GroupViewInfo
                {
                    GroupName = parentElement.FindElement(By.ClassName("group-info-name")).Text
                });
        }
    }
}