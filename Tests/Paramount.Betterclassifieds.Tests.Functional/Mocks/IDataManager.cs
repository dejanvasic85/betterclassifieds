namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface ITestDataManager
    {
        ITestDataManager Initialise();
        ITestDataManager AddOrUpdateOnlineAd(string adTitle, out int? id);
        ITestDataManager DropUserIfExists(string username);
    }
}
