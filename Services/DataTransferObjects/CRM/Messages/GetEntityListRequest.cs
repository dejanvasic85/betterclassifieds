using System;

namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    public class GetEntityListRequest : BaseRequest
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetEntityList; }
        }
    }
}
