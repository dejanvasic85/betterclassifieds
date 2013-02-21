namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserNameByEmailResponse
    {
        [DataMember]
        public string Username { get; set; }
    }
}
