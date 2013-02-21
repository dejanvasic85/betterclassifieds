using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class CreateBannerRequest : BaseRequest
    {
        public BannerEntity Banner { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.CreateBanner; }
        }
    }
}