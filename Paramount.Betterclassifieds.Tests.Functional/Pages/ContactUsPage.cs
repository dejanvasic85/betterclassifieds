using System.Linq;
using System.Threading;
using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [TestPage(RelativeUrl = "Home/ContactUs")]
    public class ContactUsPage : TestPage
    {
        public ContactUsPage(IWebDriver webdriver, IConfig config) : base(webdriver, config)
        { }

        #region Elements

        private IWebElement FullName
        {
            get { return WebDriver.FindElement(By.Id("FullName")); }
        }

        private IWebElement Email
        {
            get { return WebDriver.FindElement(By.Id("Email")); }
        }

        private IWebElement Phone
        {
            get { return WebDriver.FindElement(By.Id("Phone")); }
        }

        private IWebElement Comment
        {
            get { return WebDriver.FindElement(By.Id("Comment")); }
        }

        private IWebElement SubmitButton
        {
            get { return WebDriver.FindElement(By.Id("btnSubmit")); }
        }

        #endregion

        #region Public Driver Methods

        public ContactUsPage WithFullName(string fullName= "Default Name")
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
            SubmitButton.ClickOnElement();
            WebDriver.WaitForAjax();
            return this;
        }

        #endregion

        #region Assert Helpers

        public bool IsFirstNameRequiredMsgShown()
        {
            return WebDriver.FindElements(By.TagName("span"))
                .Any(e => e.HasAttributeValue("data-valmsg-for", "FullName"));
        }

        public bool IsEmailRequiredMsgShown()
        {
            return WebDriver.FindElements(By.TagName("span"))
                .Any(e => e.HasAttributeValue("data-valmsg-for", "Email"));
        }

        public bool IsRequiredPhoneMsgShown()
        {
            return WebDriver.FindElements(By.TagName("span"))
                .Any(e => e.HasAttributeValue("data-valmsg-for", "Phone"));
        }

        #endregion

        public bool IsHumanTestValidationMsgShown()
        {
            return IsElementPresentBy(By.Id("enquiryFailed"));
        }
    }
}