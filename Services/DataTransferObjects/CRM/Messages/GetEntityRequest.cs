using System;

namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    public class GetEntityRequest : BaseRequest
    {
        public string EntityCode { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetEntity; }
        }
    }
}
