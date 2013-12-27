namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IConfigManager
    {
        int RestrictedEditionCount { get; }
        int RestrictedOnlineDaysCount { get; }
        int NumberOfDaysAfterLastEdition { get; }
        bool IsOnlineAdFree { get; }
        string BaseUrl { get; }
        string PublisherHomeUrl { get; }
        string FacebookAppId { get; }
        string DslImageUrlHandler { get; }
        string ClientCode { get; }
    }
}