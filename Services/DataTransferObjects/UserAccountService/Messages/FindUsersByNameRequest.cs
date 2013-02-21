namespace Paramount.Common.DataTransferObjects.UserAccountService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class FindUsersByNameRequest
    {
        [DataMember(IsRequired = true)]
        public int PageSize { get; set; }

        [DataMember(IsRequired = true)]
        public int PageIndex { get; set; }

        [DataMember]
        public string UsernameToMatch { get; set; }

    }
}
