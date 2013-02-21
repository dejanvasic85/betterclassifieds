using System;

namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    public class UpdateCreateEntityRequest : BaseRequest
    {
        public string EntityCode { get; set; }
        public string EntityName { get; set; }
        public bool IsActive { get; set; }
        public int PrimaryContactId { get; set; }
        public int? TimeZone { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateCreateEntity; }
        }
    }
}
