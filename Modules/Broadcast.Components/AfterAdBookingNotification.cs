namespace Paramount.Broadcast.Components
{
    using System.Collections.ObjectModel;
    using Broadcast.UIController;
    using Broadcast.UIController.ViewObjects;
    using System.Collections.Generic;

    public class AfterAdBookingNotification : Email
    {
        private readonly string _username;
        private readonly string _content;
        private readonly Collection<EmailRecipientView> _recipients;

        public AfterAdBookingNotification(IEnumerable<string> email, string username, string content)
        {
            _username = username;
            _content = content;
            _recipients = new Collection<EmailRecipientView>();

            foreach (var item in email)
            {
                if (!string.IsNullOrEmpty(item))
                    _recipients.Add(new EmailRecipientView { Email = item });
            }

        }

        public override Collection<TemplateItemView> Fields
        {
            get
            {
                var list = new Collection<TemplateItemView>
                               {
                                   new TemplateItemView ("content",  _content),
                                   new TemplateItemView("username",_username)
                               };
                return list;
            }
        }

        public override string EmailTemplateName
        {
            get { return "AfterAdBookingNotification"; }
        }

        public override string Subject
        {
            get { return "iflog Notification"; }
        }

        public override Collection<EmailRecipientView> Recipients
        {
            get
            {
                return _recipients;
            }
        }
    }
}
