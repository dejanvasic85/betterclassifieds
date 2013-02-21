using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class RebookBannerRequest:BaseRequest
    {
        public BannerRebookDetailEntity RebookDetail { get; set; }

        public RebookBannerRequest()
        {
            RebookDetail = new BannerRebookDetailEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.RebookBanner; }
        }
    }
}