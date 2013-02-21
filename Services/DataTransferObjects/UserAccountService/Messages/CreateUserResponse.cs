namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Web.Security;
    using System.Runtime.Serialization;

    [DataContract]
    public class CreateUserResponse
    {
        [DataMember]
        public MembershipUser Membership { get; set; }

        [DataMember(IsRequired = true)]
        public MembershipCreateStatus CreateStatus { get; set; }

        public CreateUserResponse(MembershipUser membershipUser)
        {
            this.Membership = membershipUser;
        }
    }
}
