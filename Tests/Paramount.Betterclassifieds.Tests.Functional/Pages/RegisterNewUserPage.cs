using OpenQA.Selenium;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    public class RegisterNewUserPage : BasePage
    {
        public RegisterNewUserPage(IWebDriver webDriver)
            : base(webDriver)
        {

        }

        public override string RelativePath
        {
            get { return "Register.aspx"; }
        }
    }
}