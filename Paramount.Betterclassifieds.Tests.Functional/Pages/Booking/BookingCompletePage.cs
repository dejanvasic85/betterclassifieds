using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute(RelativeUrl = "Booking/Success", Title = "Booking Complete")]
    public class BookingCompletePage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public BookingCompletePage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        [FindsBy(How = How.Id, Using = "contactName"), UsedImplicitly]
        private IWebElement ContactNameField;

        [FindsBy(How = How.Id, Using = "contactEmail"), UsedImplicitly]
        private IWebElement ContactEmailField;

        [FindsBy(How = How.Id, Using = "addFriend"), UsedImplicitly]
        private IWebElement AddFriendButton;

        [FindsBy(How = How.Id, Using = "notifyFriends"), UsedImplicitly]
        private IWebElement NotifyContactsButton;

        [FindsBy(How = How.PartialLinkText, Using = "Check out my ad"), UsedImplicitly]
        private IWebElement CheckoutAdLink;

        public BookingCompletePage AddFriend(string fullName, string email)
        {
            ContactNameField.FillText(fullName);
            ContactEmailField.FillText(email);
            AddFriendButton.Click();
            _webdriver.WaitForJqueryAjax();
            return this;
        }

        public BookingCompletePage NotifyFriends()
        {
            NotifyContactsButton.Click();
            _webdriver.WaitForJqueryAjax();
            return this;
        }

        public BookingCompletePage CheckoutAd()
        {
            CheckoutAdLink.Click();
            return this;
        }
    }
}