namespace Paramount.Broadcast.Components
{
    using System.Collections.ObjectModel;
    using Broadcast.UIController;
    using Broadcast.UIController.ViewObjects;

    public class NewsletterEmail : Email
    {
        private readonly Collection<TemplateItemView> _fields;
        private readonly Collection<EmailRecipientView> _recipienetView;
        private readonly string _subject;

        public NewsletterEmail(string subject)
        {
            _fields = new Collection<TemplateItemView>();
            _recipienetView = new Collection<EmailRecipientView>();
            _subject = subject;
        }

        public override Collection<TemplateItemView> Fields
        {
            get { return _fields; }
        }

        public override string EmailTemplateName
        {
            get { return "Newsletter"; }
        }

        public override string Subject
        {
            get { return _subject; }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get { return _recipienetView; }
        }

        public bool IsHtmlBody { get; set; }

        public int Priority { get; set; }

        public override bool Send()
        {
            return EmailBroadcastController.SendNewsletter(EmailTemplateName, _subject, Recipients, Fields, Priority, IsHtmlBody);
        }
    }
}
