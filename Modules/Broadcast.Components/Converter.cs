namespace Paramount.Broadcast.Components
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Common.DataTransferObjects.Broadcast;

    public static class Converter
    {
        public static EmailRecipient Convert(this EmailRecipientView emailRecipientView)
        {
            if (emailRecipientView == null) return null;
            return new EmailRecipient
            {
                Email = emailRecipientView.Email,
                Name = emailRecipientView.Name,
                TemplateValue = emailRecipientView.TemplateFields.Convert()
            };
        }

        public static Collection<EmailRecipient> Convert(this Collection<EmailRecipientView> emailRecipients)
        {
            if (emailRecipients == null) return null;

            var list = new Collection<EmailRecipient>();
            foreach (var item in emailRecipients)
            {
                list.Add(item.Convert());
            }
            return list;
        }

        public static TemplateItem Convert(this TemplateItemView templateItemView)
        {
            return new TemplateItem
            {
                Name = templateItemView.Name,
                Value = templateItemView.Value
            };
        }

        public static TemplateItemCollection Convert(this Collection<TemplateItemView> templateItemCollection)
        {
            var list = new TemplateItemCollection();
            foreach (var item in templateItemCollection)
            {
                list.Add(item.Convert());
            }
            return list;
        }

        public static EmailTemplate Convert(this EmailTemplateView templateView)
        {
            return new EmailTemplate
            {
                Description = templateView.Description,
                EmailContent = templateView.EmailContent,
                Name = templateView.Name,
                Sender = templateView.Sender,
                Subject = templateView.Subject,
                ClientCode = templateView.ClientCode
            };
        }

        public static EmailTemplateView Convert(this EmailTemplate templateView)
        {
            return new EmailTemplateView
            {
                Description = templateView.Description,
                EmailContent = templateView.EmailContent,
                Name = templateView.Name,
                Sender = templateView.Sender,
                Subject = templateView.Subject,
                ClientCode = templateView.ClientCode
            };
        }

        public static Collection<EmailTemplateView> Convert(this Collection<EmailTemplate> templates)
        {
            var list = new Collection<EmailTemplateView>();
            foreach (var item in templates)
            {
                list.Add(item.Convert());
            }
            return list;
        }
    }
}
