using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages.Components
{
    internal class TicketDefinition
    {
        private readonly IWebElement _parentElement;

        public TicketDefinition(IWebElement parentElement)
        {
            _parentElement = parentElement;
        }

        public void SetTicketData(string ticketName, decimal price, int quantity)
        {
            _parentElement.FindElement(By.ClassName("js-ticket-name")).FillText(ticketName);
            _parentElement.FindElement(By.ClassName("js-ticket-price")).FillText(price.ToString());
            _parentElement.FindElement(By.ClassName("js-ticket-quantity")).FillText(quantity.ToString());
        }

        public void Remove()
        {
            _parentElement.FindElement(By.ClassName("js-ticket-remove")).Click();
        }
    }
}