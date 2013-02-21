namespace Paramount.Common.DataTransferObjects.Betterclassifieds.Messages
{
    using System.Collections.Generic;

    public class GetExpiredAdListByLastEditionResponse
    {
        public List<ExpiredAdEntity> ExpiryAdList { get; set; }

        public GetExpiredAdListByLastEditionResponse()
        {
            ExpiryAdList = new List<ExpiredAdEntity>();
        }
    }
}