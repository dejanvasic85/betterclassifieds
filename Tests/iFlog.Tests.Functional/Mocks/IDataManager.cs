namespace iFlog.Tests.Functional.Mocks
{
    public interface IDataManager
    {
        IDataManager Initialise();
        int CreateAd(string adTitle);
    }
}
