using System;

namespace Paramount.Common.DataTransferObjects.Broadcast.Messages
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using DataTransferObjects;
    using DataTransferObjects.Broadcast;

    [DataContract]
    public class SendEmailRequest:BaseRequest
    {
        [DataMember]
        public TemplateItemCollection TemplateValue { get; set; }

        [DataMember]
        public Collection<EmailRecipient> Recipients { get; set; }

        [DataMember(IsRequired=true)]
        public bool IsBodyHtml { get; set; }

        [DataMember]
        public EmailPriority Priority { get; set; }

        public SendEmailRequest()
        {
            TemplateValue = new TemplateItemCollection();
            Recipients = new Collection<EmailRecipient>();
        }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string EmailTemplate { get; set; }

        public override string TransactionName
        {
            get { return AuditTransactions.SendEmail; }
        }
    }
}