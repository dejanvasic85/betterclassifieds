namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;

    public class FunctionExistsRequest:BaseRequest
    {
        public string FunctionName { get; set; }
        public override string TransactionName
        {
            get { return AuditTransactions.FunctionExists; }
        }
    }
}