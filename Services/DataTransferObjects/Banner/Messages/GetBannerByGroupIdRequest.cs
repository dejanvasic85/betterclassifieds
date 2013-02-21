using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetBannerByGroupIdRequest : BaseRequest
    {
        public Guid GroupId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetBannerByGroupId; }
        }
    }
}