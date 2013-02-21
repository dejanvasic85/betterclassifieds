namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using Broadcast;

    [DataContract]
    public class GetEmailTemplateListResponse
    {
        [DataMember(IsRequired = true)]
        public Collection<EmailTemplate> TemplateList { get; set; }

        public GetEmailTemplateListResponse()
        {
            TemplateList = new Collection<EmailTemplate>();
        }
    }
}