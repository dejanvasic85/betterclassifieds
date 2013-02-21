namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;
    using System.Web.Security;

    [DataContract]
    public class UpdateUserRequest : MembershipProviderBaseRequest
    {
        [DataMember]
        public MembershipUser User { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.UpdateUserRequest; }
        }
    }
}
