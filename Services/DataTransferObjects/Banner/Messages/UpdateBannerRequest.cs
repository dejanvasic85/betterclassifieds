using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class UpdateBannerRequest : BaseRequest
    {
        public BannerModifyEntity Banner { get; set; }

        public UpdateBannerRequest()
        {
            Banner = new BannerModifyEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateBanner; }
        }
    }
}