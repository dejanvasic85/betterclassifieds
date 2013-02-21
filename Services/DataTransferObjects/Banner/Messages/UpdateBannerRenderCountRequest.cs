using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class UpdateBannerRenderCountRequest:BaseRequest 
    {
        public Guid BannerId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateBannerRenderCountRequest; }
        }
    }
}