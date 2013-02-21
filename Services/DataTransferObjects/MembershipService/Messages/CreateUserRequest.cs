namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateUserRequest : MembershipProviderBaseRequest
    {

        public CreateUserRequest()
        {
            ProfileInformation = new ProfileInfo();
        }
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string SecurityQuestion { get; set; }

        [DataMember]
        public string SecurityAnswer { get; set; }

        [DataMember(IsRequired = true)]
        public bool IsApproved { get; set; }

        [DataMember]
        public Guid? ProviderUserKey { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.CreateUserRequest; }
        }

        [DataMember]
        public string PasswordQuestion { get; set; }

        [DataMember]
        public string PasswordAnswer { get; set; }

        [DataMember]
        public ProfileInfo ProfileInformation { get; set; }
    }
}
