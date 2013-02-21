using System;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class LogBannerAuditRequest : BaseRequest
    {
        public BannerAuditEntity Audit { get; set; }

        public LogBannerAuditRequest()
        {
            Audit = new BannerAuditEntity();
        }

        public override string TransactionName
        {
            get { return AuditTransactions.LogBannerAudit; }
        }
    }
}