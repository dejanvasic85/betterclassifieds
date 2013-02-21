namespace Paramount.Common.DataTransferObjects.UserAccountWebService.Messages
{
    using System.Runtime.Serialization;

    [DataContract]
    public class GetNewsletterUsersRequest
    {
        [DataMember(IsRequired = true)]
        public int PageSize { get; set; }

        [DataMember(IsRequired = true)]
        public int PageIndex { get; set; }

        [DataMember(IsRequired = true)]
        public bool IncludeAll { get; set; }

        [DataMember]
        public string UsernameMatch { get; set; }
    }
}
