namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;
    using Paramount.ApplicationBlock.Membership;

    [DataContract]
    public class ParamountMembershipUserResponse
    {
        [DataMember]
        public ParamountMembershipUser User { get; set; }
    }
}
