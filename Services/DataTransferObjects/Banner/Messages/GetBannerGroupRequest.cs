using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetBannerGroupRequest : BaseRequest
    {
        public Guid BannerGroupId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetBannerGroup; }
        }
    }
}