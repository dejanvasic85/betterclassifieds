using System.Linq;

namespace Paramount.Services
{
    
    using Common.DataTransferObjects.Betterclassifieds.Messages;
    using Common.ServiceContracts;
    using Common.DataTransferObjects.Betterclassifieds;
    using DataService;

    public class BetterclassifiedService : BaseService, IBetterclassifiedService
    {
        public GetExpiredAdListByLastEditionResponse GetExpiredAdListByLastEdition(GetExpiredAdListByLastEditionRequest request)
        {
            
            var expiryAdList = BetterclassifiedDataService.GetExpiredAdByLastEdition(request.EditionDate);
            var response = new GetExpiredAdListByLastEditionResponse();
            foreach (var item in expiryAdList)
            {
                response.ExpiryAdList.Add(item.Convert());
            }
            
            return response;
        }

        public GetActivitySummaryResponse GetActivitySummary(GetActivitySummaryRequest request)
        {
            
            
            ActivitySummary summary = BetterclassifiedDataService
                .GetActivitySummaries(request.ReportDate)
                .Select(row => new ActivitySummary
                {
                    NumberOfBookings = row.TotalBookings,
                    SumOfBookings = row.TotalIncome
                })
                .First();

            return new GetActivitySummaryResponse { ActivitySummary = summary };
        }
    }
}