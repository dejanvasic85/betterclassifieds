namespace Paramount.DomainModel.Business.Repositories
{
    public interface IConfigSettings
    {
        int RestrictedEditionCount { get; }
        int RestrictedOnlineDaysCount { get; }
        int NumberOfDaysAfterLastEdition { get; }
        bool IsOnlineAdFree { get; }
        string BaseUrl { get; }
        string FacebookAppId { get; }
        string DslImageUrlHandler { get; }
        string ClientCode { get; }
    }
}