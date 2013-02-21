namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;
    using System.Web.Security;

    [DataContract]
    public class GetUsersResponse
    {
        [DataMember(IsRequired = true)]
        public int TotalRecord { get; set; }

        [DataMember]
        public MembershipUserCollection Membership { get; set; }
    }
}
