namespace Paramount.Common.DataTransferObjects.Betterclassifieds.Messages
{
    using System;

    public class GetExpiredAdListByLastEditionRequest : BaseRequest
    {
        public DateTime EditionDate { get; set; }
        public override string TransactionName
        {
            get { return BetterclassifiedTransactions.GetExpiredAdListByLastEdition; }
        }
    }
}