namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserNameByEmailRequest
    {
        [DataMember]
        public string Email { get; set; }
    }
}
