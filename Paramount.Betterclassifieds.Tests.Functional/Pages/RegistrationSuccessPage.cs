using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Account/Register")]
    public class RegistrationSuccessPage : TestPage
    {
        public RegistrationSuccessPage(IWebDriver webdriver, IConfig config)
            : base(webdriver, config)
        { }

        #region Page Elements

        [FindsBy(How = How.Id, Using = "thankYouHeading"), UsedImplicitly]
        private IWebElement ThankYouHeading;

        [FindsBy(How = How.Id, Using = "confirmationLink"), UsedImplicitly]
        private IWebElement Confirmation;
        #endregion

        #region Driving Methods

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