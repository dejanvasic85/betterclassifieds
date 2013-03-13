namespace Paramount.Broadcast.Components
{
    using System.Collections.ObjectModel;

    public class OnlineAdEnquiryNotification : Email
    {
        private readonly Collection<EmailRecipientView> _recipients;

        public OnlineAdEnquiryNotification(string recipientEmail)
        {
            _recipients = new Collection<EmailRecipientView>();
            _recipients.Add(new EmailRecipientView { Email = recipientEmail });
        }

        public string AdNumber { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public string Phone { get; set; }

        public override Collection<TemplateItemView> Fields
        {
            get
            {
                var list = new Collection<TemplateItemView>
                               {
                                   new TemplateItemView ("adNumber",  AdNumber),
                                   new TemplateItemView ("fullName",  FullName),
                                   new TemplateItemView ( "email", EmailAddress),
                                   new TemplateItemView ( "content", Message),
                                   new TemplateItemView ( "phone", Phone)
                               };

                return list;
            }
        }

        public override string EmailTemplateName
        {
            get { return "OnlineAdEnquiry"; }
        }

        public override string Subject
        {
            get { return string.Format("iFlog Online Ad Enquiry for iflog ID: {0} - {1}", AdNumber, FullName); }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get { return _recipients; }
        }
    }
}
