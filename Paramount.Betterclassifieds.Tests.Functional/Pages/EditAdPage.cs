using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Properties;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute("EditAd/Details/{0}")]
    public class EditAdPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public EditAdPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        [FindsBy(How = How.Id, Using = "OnlineAdHeading"), UsedImplicitly]
        private IWebElement HeadingElement { get; set; }

        [FindsBy(How = How.Id, Using = "btnUpdateAd"), UsedImplicitly] 
        private IWebElement SubmitButtonElement;
        
        public EditAdPage WithTitle(string newAdTitle)
        {
            HeadingElement.FillText(newAdTitle);
            return this;
        }

        public EditAdPage SubmitChanges()
        {
            SubmitButtonElement.ClickOnElement();
            return this;
        }

        public bool DisplaysSuccessMessage()
        {
            return _webDriver.FindElements(By.ClassName("alert-success")).Any();
        }
    }
}