namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
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

    }
}
