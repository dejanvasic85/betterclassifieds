namespace Paramount.Common.DataTransferObjects.MembershipService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ValidateUserResponse
    {
        [DataMember(IsRequired = true)]
        public bool Success { get; set; }
    }
}
