namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ValidateUserRequest : MembershipProviderBaseRequest
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.ValidateUserRequest; }
        }
    }
}
