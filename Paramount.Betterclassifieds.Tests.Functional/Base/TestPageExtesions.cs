namespace Paramount.Betterclassifieds.Tests.Functional
{
    public static class TestPageExtesions
    {
        public static ITestPage InitialiseElements(this ITestPage testPage)
        {
            OpenQA.Selenium.Support.PageObjects.PageFactory.InitElements(testPage.GetDriver(), testPage);
            return testPage;
        }
    }
}