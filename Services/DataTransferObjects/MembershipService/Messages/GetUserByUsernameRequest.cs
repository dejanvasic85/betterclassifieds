namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserByUsernameRequest : MembershipProviderBaseRequest
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember(IsRequired = true)]
        public bool UserIsOnline { get; set; }

        public override string TransactionName
        {
            get { return  AuditTransactions.GetUserByUsernameRequest; }
        }
    }
}
