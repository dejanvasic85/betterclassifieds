namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ChangePasswordRequest : MembershipProviderBaseRequest 
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string OldPassword { get; set; }

        [DataMember]
        public string NewPassword { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.ChangePasswordRequest; }
        }
    }
}
