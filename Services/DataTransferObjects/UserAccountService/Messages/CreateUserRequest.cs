namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateUserRequest : BaseRequest
    {
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
        public Guid ProviderUserKey { get; set; }

        public override string TransactionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
