using System;

namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    public class GetModuleListRequest : BaseRequest
    {
        public int? ModuleId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetModuleList; }
        }
    }
}
