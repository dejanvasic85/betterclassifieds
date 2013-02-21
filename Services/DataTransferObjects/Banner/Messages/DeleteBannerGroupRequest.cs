using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class DeleteBannerGroupRequest : BaseRequest
    {
        public BannerGroupEntity BannerGroup { get; set; }
        public DeleteBannerGroupRequest ()
        {
            this.BannerGroup = new BannerGroupEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.CreateBannerGroup; }
        }
    }
}