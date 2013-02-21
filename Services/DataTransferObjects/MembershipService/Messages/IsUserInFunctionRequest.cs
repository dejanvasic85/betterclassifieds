namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;

    public class IsUserInFunctionRequest:BaseRequest 
    {
        public string Username { get; set; }
        public string FunctionName { get; set; }
        public override string TransactionName
        {
            get { return AuditTransactions.IsUserInFunction; }
        }
    }
}