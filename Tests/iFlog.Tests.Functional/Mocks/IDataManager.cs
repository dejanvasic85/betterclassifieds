namespace iFlog.Tests.Functional.Mocks
{
    /// <summary>
    /// Used directly by the tests to setup and assert scenarios
    /// </summary>
    public interface IDataManager
    {
        IDataManager Initialise();
        int AddOrUpdateOnlineAd(string adTitle);
    }
}
