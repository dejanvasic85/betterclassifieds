using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class UpdateBannerClickCountRequest:BaseRequest 
    {
        public Guid BannerId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateBannerClickCountRequest; }
        }
    }
}