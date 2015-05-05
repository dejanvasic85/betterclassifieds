using NUnit.Framework;
using Paramount.Betterclassifieds.Tests.Functional.Pages;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class ChangePasswordSteps
    {
        private readonly PageBrowser _pageBrowser;

        public ChangePasswordSteps(PageBrowser pageBrowser)
        {
            _pageBrowser = pageBrowser;
        }

        [When(@"Set the old password ""(.*)"" and new password ""(.*)""")]
        public void WhenSetTheOldPasswordAndNewPassword(string oldPassword, string newPassword)
        {
            _pageBrowser.Init<ChangePasswordTestPage>()
                .SetOldPassword(oldPassword)
                .SetNewPassword(newPassword)
                .Submit();
        }

        [When(@"Wait until success message is displayed")]
        public void WhenWaitUntilSuccessMessageIsDisplayed()
        {
            _pageBrowser.Init<ChangePasswordTestPage>()
                .WaitForSuccessMsg();
        }

        [Then(@"the success message contain ""(.*)""")]
        public void ThenTheSuccessMessageShouldBe(string expectedMsg)
        {
            var actualMsg = _pageBrowser.Init<ChangePasswordTestPage>()
                .GetSuccessMessage();

            Assert.That(actualMsg, Is.StringContaining(expectedMsg));
        }
    }
}
