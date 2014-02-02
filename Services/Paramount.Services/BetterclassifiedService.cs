using System.Linq;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Services
{
    
    using Common.DataTransferObjects.Betterclassifieds.Messages;
    using Common.ServiceContracts;
    using Common.DataTransferObjects.Betterclassifieds;
    
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
    }
}