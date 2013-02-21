namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserByIdRequest : MembershipProviderBaseRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember(IsRequired = true)]
        public bool UserIsOnline { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.GetUserByIdRequest; }
        }
    }
}
