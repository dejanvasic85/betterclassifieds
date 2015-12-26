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

        [FindsBy(How = How.ClassName, Using = "alert-success"), UsedImplicitly]
        private IWebElement SuccessMessage;

        [FindsBy(How = How.Id, Using = "PayPalEmail"), UsedImplicitly]
        private IWebElement PayPalEmailInput;

        [FindsBy(How = How.Id, Using = "BankName"), UsedImplicitly]
        private IWebElement BankNameInput;

        [FindsBy(How = How.Id, Using = "BankAccountName"), UsedImplicitly]
        private IWebElement BankAccountNameInput;

        [FindsBy(How = How.Id, Using = "BankBsbNumber"), UsedImplicitly]
        private IWebElement BankBsbNumberInput;

        [FindsBy(How = How.Id, Using = "BankAccountNumber"), UsedImplicitly]
        private IWebElement BankAccountNumberInput;

        [FindsBy(How = How.CssSelector, Using = "[data-payment=PayPal]"), UsedImplicitly]
        private IWebElement PayPalPreferredButton;

        [FindsBy(How = How.CssSelector, Using = "[data-payment=DirectDebit]"), UsedImplicitly]
        private IWebElement DirectDebitPreferredButton;


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

        public AccountDetailsTestPage WithPayPalEmail(string email)
        {
            PayPalEmailInput.FillText(email);
            return this;
        }

        public AccountDetailsTestPage WithBankDetails(string bankName,
            string bsb, string accountNumber, string accountName)
        {
            BankNameInput.FillText(bankName);
            BankBsbNumberInput.FillText(bsb);
            BankAccountNumberInput.FillText(accountNumber);
            BankAccountNameInput.FillText(accountName);
            return this;
        }
        
        public AccountDetailsTestPage WithDirectDebitPaymentMethod()
        {
            DirectDebitPreferredButton.ClickOnElement();
            return this;
        }

        public AccountDetailsTestPage WithPayPalPaymentMethod()
        {
            PayPalPreferredButton.ClickOnElement();
            return this;
        }

        public bool IsValidationDisplayed()
        {
            return _webdriver.FindElement(By.ClassName("alert-danger")).Displayed;
        }
    }
}