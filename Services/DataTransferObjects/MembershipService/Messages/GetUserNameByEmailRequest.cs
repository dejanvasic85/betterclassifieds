namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserNameByEmailRequest
    {
        [DataMember]
        public string Email { get; set; }
    }
}
