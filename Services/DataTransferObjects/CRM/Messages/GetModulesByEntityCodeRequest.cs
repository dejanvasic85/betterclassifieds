using System;

namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    public class GetModulesByEntityCodeRequest : BaseRequest {
        public override string TransactionName
        {
            get { return AuditTransactions.GetModulesByEntityCode; }
        }
    }
}
