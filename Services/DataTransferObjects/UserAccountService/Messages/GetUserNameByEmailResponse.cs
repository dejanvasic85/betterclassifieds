namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetUserNameByEmailResponse
    {
        [DataMember]
        public string Username { get; set; }
    }
}
