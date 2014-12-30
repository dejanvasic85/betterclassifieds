using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Account/Details")]
    public class AccountDetailsTestPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public AccountDetailsTestPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        #region Elements

        [FindsBy(How = How.Id, Using = "AddressLine1"), UsedImplicitly]
        private IWebElement AddressLine1;

        [FindsBy(How = How.Id, Using = "Phone"), UsedImplicitly]
        private IWebElement PhoneNumber;

        [FindsBy(How = How.Id, Using = "btnRegister"), UsedImplicitly]
        private IWebElement SubmitButton;

        [FindsBy(How = How.ClassName, Using="alert-success"), UsedImplicitly]
        private IWebElement SuccessMessage;

        #endregion

        public AccountDetailsTestPage WithAddressLine1(string addressLine1)
        {
            AddressLine1.FillText(addressLine1);
            return this;
        }

        public AccountDetailsTestPage WithPhone(string phone)
        {
            PhoneNumber.FillText(phone);
            return this;
        }

        public AccountDetailsTestPage Submit()
        {
            SubmitButton.ClickOnElement();
            return this;
        }

        public bool IsSuccessMessageDisplayed()
        {
            return SuccessMessage.Text.HasValue();
        }
    }
}