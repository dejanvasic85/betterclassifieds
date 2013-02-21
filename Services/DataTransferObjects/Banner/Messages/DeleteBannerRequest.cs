using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class DeleteBannerRequest : BaseRequest
    {
       public BannerEntity Banner { get; set; }

        public void  DeleteBannerGroupRequest ()
        {
            Banner = new BannerEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.DeleteBanner; }
        }
    }
}