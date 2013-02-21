namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;

    public class GetProfileRequest:BaseRequest
    {
        public override string TransactionName
        {
            get { return AuditTransactions.GetProfile; }
        }

        public string Username { get; set; }
    }
}