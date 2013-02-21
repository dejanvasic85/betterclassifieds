using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class UpdateBannerGroupRequest : BaseRequest
    {
        public BannerGroupEntity BannerGroup { get; set; }

        public UpdateBannerGroupRequest()
        {
            BannerGroup = new BannerGroupEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateBannerGroup; }
        }
    }
}