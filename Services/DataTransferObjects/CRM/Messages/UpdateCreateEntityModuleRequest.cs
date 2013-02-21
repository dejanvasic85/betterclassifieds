namespace Paramount.Common.DataTransferObjects.CRM.Messages
{
    using System;

    public class UpdateCreateEntityModuleRequest : BaseRequest
    {
        public int? EntityModuleId { get; set; }
        public bool Active { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ModuleId { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateCreateEntityModule; }
        }
    }
}
