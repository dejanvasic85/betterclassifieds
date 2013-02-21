namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;

    public class GetFunctionsForUserRequest:BaseRequest 
    {
        public override string TransactionName
        {
            get { return AuditTransactions.GetFunctionsForUser; }
        }

        public string Username { get; set; }
    }
}