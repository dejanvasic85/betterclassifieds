namespace Paramount.Common.DataTransferObjects.UserAccountWebService.Messages
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using UserAccountService;

    [DataContract]
    public class GetNewsletterUsersResponse
    {
        [DataMember]
        public List<UserAccountProfile> Profiles { get; set; }

        [DataMember(IsRequired = true)]
        public int TotalCount
        {
            get;
            set;
        }

        public GetNewsletterUsersResponse()
        {
            Profiles = new List<UserAccountProfile>();
        }
    }
}
