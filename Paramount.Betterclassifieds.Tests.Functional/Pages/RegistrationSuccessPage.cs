using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Account/Register")]
    public class RegistrationSuccessPage : BaseTestPage
    {
        public RegistrationSuccessPage(IWebDriver webdriver, IConfig config) 
            : base(webdriver, config)
        { }

        #region Page Elements

        private IWebElement ThankYouHeading
        {
            get { return FindElement(By.Id("thankYouHeading")); }
        }

        private IWebElement Confirmation
        {
            get { return FindElement(By.Id("confirmationLink")); }
        }

        #endregion

        #region Public Methods

        public string GetThankYouHeading()
        {
            return ThankYouHeading.Text;
        }

        public string GetConfirmationText()
        {
            return Confirmation.Text;
        }

        #endregion
    }
}