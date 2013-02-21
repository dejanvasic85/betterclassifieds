namespace Paramount.Services
{
    using System;
    using System.Configuration;
    using ApplicationBlock.Configuration;
    using ApplicationBlock.Logging.AuditLogging;
    using Common.DataTransferObjects.Betterclassifieds.Messages;
    using Common.DataTransferObjects.LoggingService.Messages;
    using Common.ServiceContracts;
    using DataService;

    public class BetterclassifiedService : BaseService, IBetterclassifiedService
    {
        public GetExpiredAdListByLastEditionResponse GetExpiredAdListByLastEdition(GetExpiredAdListByLastEditionRequest request)
        {
            request.LogRequestAudit();

            var expiryAdList = BetterclassifiedDataService.GetExpiredAdByLastEdition(request.EditionDate);

            var response = new GetExpiredAdListByLastEditionResponse();
            foreach (var item in expiryAdList)
            {
                response.ExpiryAdList.Add(item.Convert());
            }

            AuditLogManager.Log(request.ConvertToAudit(response));

            return response;
        }
    }
}