using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Properties;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Account/Register")]
    public class RegistrationConfirmationPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public RegistrationConfirmationPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        #region Page Elements

        [FindsBy(How = How.Id, Using = "confirmationHeading"), UsedImplicitly]
        private IWebElement ConfirmationHeading;

        [FindsBy(How = How.Id, Using = "confirmationMessage"), UsedImplicitly]
        private IWebElement ConfirmationMessage;

        [FindsBy(How = How.Id, Using = "token"), UsedImplicitly]
        private IWebElement TokenTextBox;

        [FindsBy(How = How.ClassName, Using = "btn-default"), UsedImplicitly]
        private IWebElement SubmitButton;
        #endregion

        #region Driving Methods

        public string GetThankYouHeading()
        {
            return ConfirmationHeading.Text;
        }

        public string GetConfirmationText()
        {
            return ConfirmationMessage.Text;
        }

        public RegistrationConfirmationPage SetToken(string tokenValue)
        {
            TokenTextBox.FillText(tokenValue);
            return this;
        }

        public RegistrationConfirmationPage Submit()
        {
            SubmitButton.ClickOnElement();
            return this;
        }
        
        public bool IsCodeTextboxAvailable()
        {
            return this.SubmitButton != null;
        }

        #endregion

    }
}