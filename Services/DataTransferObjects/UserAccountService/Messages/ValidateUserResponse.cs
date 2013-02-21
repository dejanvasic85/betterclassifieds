namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ValidateUserResponse
    {
        [DataMember(IsRequired = true)]
        public bool Success { get; set; }
    }
}
