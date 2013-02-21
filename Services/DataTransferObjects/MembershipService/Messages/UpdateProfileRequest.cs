namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;

    public class UpdateProfileRequest:BaseRequest
    {
        public string Username { get; set; }
        public ProfileInfo Profile { get; set; }
        public override string TransactionName
        {
            get { return AuditTransactions.UpdateProfile; }
        }
    }
}