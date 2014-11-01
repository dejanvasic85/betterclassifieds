using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Paramount.Betterclassifieds.Tests.Functional.Annotations;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Home/ContactUs")]
    public class ContactUsPage : ITestPage
    {
        private readonly IWebDriver _webdriver;

        public ContactUsPage(IWebDriver webdriver)
        {
            _webdriver = webdriver;
        }

        #region Elements

        [FindsBy(How = How.Id, Using = "FullName"), UsedImplicitly]
        private IWebElement FullName;

        [FindsBy(How = How.Id, Using = "Email"), UsedImplicitly]
        private IWebElement Email;

        [FindsBy(How = How.Id, Using = "Phone"), UsedImplicitly]
        private IWebElement Phone;

        [FindsBy(How = How.Id, Using = "Comment"), UsedImplicitly]
        private IWebElement Comment;

        [FindsBy(How = How.Id, Using = "btnSubmit"), UsedImplicitly]
        private IWebElement SubmitButton;

        #endregion

        #region Public Driver Methods

        public ContactUsPage WithFullName(string fullName = "Default Name")
        {
            FullName.FillText(fullName);
            return this;
        }

        public ContactUsPage WithEmail(string email = "default@email.com")
        {
            Email.FillText(email);
            return this;
        }

        public ContactUsPage WithPhone(string phone = "03 9999 1111")
        {
            Phone.FillText(phone);
            return this;
        }

        public ContactUsPage WithComments(string comments = "These are default comments")
        {
            Comment.FillText(comments);
            return this;
        }

        public ContactUsPage Submit()
        {
            Thread.Sleep(2000); // Sometimes google maps take a while to load
            SubmitButton.ClickOnElement();
            _webdriver.WaitForJqueryAjax(10);
            this.InitialiseElements();
            return this;
        }

        #endregion

        #region Assert Helpers

        public bool IsFirstNameRequiredMsgShown()
        {
            return _webdriver.FindElements(By.TagName("span"))
                .Any(e => e.HasAttributeValue("data-valmsg-for", "FullName"));
        }

        public bool IsEmailRequiredMsgShown()
        {
            return _webdriver.FindElements(By.TagName("span"))
                .Any(e => e.HasAttributeValue("data-valmsg-for", "Email"));
        }

        public bool IsRequiredPhoneMsgShown()
        {
            return _webdriver.FindElements(By.TagName("span"))
                .Any(e => e.HasAttributeValue("data-valmsg-for", "Phone"));
        }

        public bool IsHumanTestValidationMsgShown()
        {
            return _webdriver.IsElementPresentBy(By.Id("enquiryFailed"));
        }

        #endregion

        public IWebDriver GetDriver()
        {
            return _webdriver;
        }
    }
}