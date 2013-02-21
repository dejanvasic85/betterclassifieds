namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System.Runtime.Serialization;
    using Broadcast;

    [DataContract]
    public class GetEmailTemplateResponse
    {
        [DataMember]
        public EmailTemplate Template { get; set; }

        public GetEmailTemplateResponse()
        {
            this.Template = new EmailTemplate();
        }
    }
}