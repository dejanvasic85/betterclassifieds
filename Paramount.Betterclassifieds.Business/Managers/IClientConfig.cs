using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models;

namespace Paramount.Betterclassifieds.Business.Managers
{
    public interface IClientConfig
    {
        int RestrictedEditionCount { get; }
        int RestrictedOnlineDaysCount { get; }
        int NumberOfDaysAfterLastEdition { get; }
        string FacebookAppId { get; }
        int SearchResultsPerPage { get; }
        int SearchMaxPagedRequests { get; }
        Address ClientAddress { get; }
        string[] SupportEmailList { get; }
        int? MaxOnlineImages { get; }

    }
}