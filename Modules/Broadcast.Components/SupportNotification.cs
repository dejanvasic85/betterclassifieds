namespace Paramount.Broadcast.Components
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Broadcast.UIController.ViewObjects;

    public class SupportNotification : Email
    {
        private readonly Collection<EmailRecipientView> _recipients;
        private readonly string _content;
        private readonly string _supportTypeName;
        private readonly string _sender;

        public SupportNotification(string sender, string content, string supportTypeName, IEnumerable<string> recipients)
        {
            _recipients = new Collection<EmailRecipientView>();
            _supportTypeName = supportTypeName;
            _content = content;
            _sender = sender;

            foreach (var item in recipients.Where(item => !string.IsNullOrEmpty(item)))
            {
                _recipients.Add(new EmailRecipientView { Email = item });
            }
        }

        public override Collection<TemplateItemView> Fields
        {
            get
            {
                var list = new Collection<TemplateItemView> { new TemplateItemView("content", _content) };

                return list;
            }
        }

        public override string EmailTemplateName
        {
            get { return "SupportNotification"; }
        }

        public override string Subject
        {
            get { return string.Format("iFlog Support - {0} from {1}", _supportTypeName, _sender); }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get { return _recipients; }
        }
    }
}
