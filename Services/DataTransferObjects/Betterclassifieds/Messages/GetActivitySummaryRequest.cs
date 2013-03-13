using System;

namespace Paramount.Common.DataTransferObjects.Betterclassifieds.Messages
{
    public class GetActivitySummaryRequest : BaseRequest
    {
        public DateTime ReportDate { get; set; }
        public override string TransactionName
        {
            get { return BetterclassifiedTransactions.GetActivitySummary; }
        }
    }
}