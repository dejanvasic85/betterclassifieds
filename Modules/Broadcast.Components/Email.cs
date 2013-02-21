namespace Paramount.Broadcast.Components
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Broadcast.UIController.ViewObjects;
    using Broadcast.UIController;

    public abstract class Email
    {
        public abstract Collection<TemplateItemView> Fields { get; }
        public abstract string EmailTemplateName { get; }
        public abstract string Subject { get; }
        public abstract Collection<EmailRecipientView> Recipients { get; }
        
        public virtual bool Send()
        {
            return EmailBroadcastController.SendEmail(EmailTemplateName, Recipients, Fields, Subject);
        }
    }
}
