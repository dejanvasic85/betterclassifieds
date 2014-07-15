using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IClientConfig
    {
        int RestrictedEditionCount { get; }
        int RestrictedOnlineDaysCount { get; }
        int NumberOfDaysAfterLastEdition { get; }
        bool IsOnlineAdFree { get; }
        string PublisherHomeUrl { get; }
        string FacebookAppId { get; }
        int SearchResultsPerPage { get; }
        int SearchMaxPagedRequests { get; }
        Address ClientAddress { get; }
    }
}