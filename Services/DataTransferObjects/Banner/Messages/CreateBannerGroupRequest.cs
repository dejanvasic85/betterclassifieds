using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class CreateBannerGroupRequest : BaseRequest
    {
        public BannerGroupEntity BannerGroup { get; set; }

        public CreateBannerGroupRequest()
        {
            BannerGroup = new BannerGroupEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.CreateBannerGroup; }
        }
    }
}