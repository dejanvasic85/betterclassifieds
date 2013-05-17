namespace Paramount.Broadcast.Components
{
    using System.Collections.ObjectModel;

    public class AdExpiryNotification : Email
    {
        private readonly Collection<EmailRecipientView> _recipients;
        private readonly Collection<TemplateItemView> _fields;
        public AdExpiryNotification()
        {
            _recipients = new Collection<EmailRecipientView>();
            _fields = new Collection<TemplateItemView>();
        }

        public override Collection<TemplateItemView> Fields
        {
            get
            {
                return _fields;
            }
        }

        public override string EmailTemplateName
        {
            get { return "Notification"; }
        }

        public override string Subject
        {
            get { return "iFlog - Extend your expiring ad!"; }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get { return _recipients; }
        }
    }
}
