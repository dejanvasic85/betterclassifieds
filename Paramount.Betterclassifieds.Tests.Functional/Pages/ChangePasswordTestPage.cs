using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Properties;

namespace Paramount.Betterclassifieds.Tests.Functional.Pages
{
    [NavRoute("Account/ChangePassword")]
    public class ChangePasswordTestPage : ITestPage
    {
        private readonly IWebDriver _webDriver;

        public ChangePasswordTestPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        #region Elements

        [FindsBy(How = How.Id, Using = "OldPassword"), UsedImplicitly]
        private IWebElement OldPasswordElement;

        [FindsBy(How = How.Id, Using = "NewPassword"), UsedImplicitly]
        private IWebElement NewPassword;

        [FindsBy(How = How.Id, Using = "ConfirmNewPassword"), UsedImplicitly]
        private IWebElement ConfirmNewPassword;

        [FindsBy(How = How.Id, Using = "btnSubmit"), UsedImplicitly]
        private IWebElement btnSubmit;

        [FindsBy(How = How.ClassName, Using = "alert-success"), UsedImplicitly]
        private IWebElement successMsg;


        #endregion

        public ChangePasswordTestPage SetOldPassword(string password)
        {
            this.OldPasswordElement.FillText(password);
            return this;
        }

        public ChangePasswordTestPage SetNewPassword(string password, bool setConfirm = true)
        {
            this.NewPassword.FillText(password);
            if (setConfirm)
            {
                this.ConfirmNewPassword.FillText(password);
            }
            return this;
        }

        public ChangePasswordTestPage Submit()
        {
            this.btnSubmit.ClickOnElement();
            return this;
        }

        public ChangePasswordTestPage WaitForSuccessMsg()
        {
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            successMsg = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("alert-success")));

            return this;
        }

        public string GetSuccessMessage()
        {
            return successMsg.Text;
        }
    }
}