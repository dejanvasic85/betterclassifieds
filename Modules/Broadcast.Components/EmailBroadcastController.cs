namespace Paramount.Broadcast.Components
{
    using System;
    using System.Collections.ObjectModel;
    using Common.DataTransferObjects.Broadcast;
    using Common.DataTransferObjects.Broadcast.Messages;
    using System.Collections.Generic;
    using ApplicationBlock.Configuration;
    using Services.Proxy;

    public class EmailBroadcastController
    {
        public static void InsertUpdateTemplate(EmailTemplateView templateView, bool update)
        {
            var request = new InsertUpdateTemplateRequest
                {
                    Name = templateView.Name,
                    ClientCode = templateView.ClientCode,
                    ApplicationName = ConfigSettingReader.ApplicationName,
                    EmailContent = templateView.EmailContent,
                    Description = templateView.Description,
                    Sender = templateView.Sender,
                    Subject = templateView.Subject,
                    Update = update
                };

            WebServiceHostManager.BroadcastServiceHost.InsertUpdateTemplate(request);

        }

        public static EmailTemplateView GetTemplate(string templateName)
        {
            var request = new GetEmailTemplateRequest { TemplateName = templateName };
            var response = WebServiceHostManager.BroadcastServiceHost.GetEmailTemplate(request);
            return response.Template.Convert();
        }

        public static bool SendEmail(string templateName, Collection<EmailRecipientView> recipients, Collection<TemplateItemView> fields, string subject)
        {
            var request = new SendEmailRequest
            {
                TemplateValue = fields.Convert(),
                EmailTemplate = templateName,
                Priority = EmailPriority.Normal,
                IsBodyHtml = true,
                ClientCode = ConfigSettingReader.ClientCode,
                ApplicationName = ConfigSettingReader.ApplicationName,
                Domain = ConfigSettingReader.ClientCode,
                Recipients = recipients.Convert(),
                Subject = subject
            };
            WebServiceHostManager.BroadcastServiceHost.SendMail(request);
            return true;
        }

        public static bool SendNewsletter(string templateName, string subject, Collection<EmailRecipientView> recipients, Collection<TemplateItemView> fields, int emailPriority, bool isBodyHtml)
        {
            var request = new SendNewsLetterRequest
            {
                TemplateValue = fields.Convert(),
                EmailTemplate = templateName,
                Priority = (EmailPriority)emailPriority,
                IsBodyHtml = isBodyHtml,
                ClientCode = ConfigSettingReader.ClientCode,
                ApplicationName = ConfigSettingReader.ApplicationName,
                Domain = ConfigSettingReader.ClientCode,
                Recipients = recipients.Convert(),
                Subject = subject
            };
            WebServiceHostManager.BroadcastServiceHost.SendNewsletter(request);
            return true;
        }

        public static void ProcessEmailBroadcast(Guid? broadcastId)
        {
            var request = new ProcessEmailsRequest { BroadcastId = broadcastId };
            WebServiceHostManager.BroadcastServiceHost.Process(request);
        }

        public static IEnumerable<EmailTemplateView> GetTemplatesForEntity(string clientCode)
        {
            var request = new GetEmailTemplateListRequest { ClientCode = clientCode };
            var response = WebServiceHostManager.BroadcastServiceHost.GetEmailTemplateListByClient(request);
            response.TemplateList.Insert(0, new EmailTemplate { Description = "[Select Template]", Name = string.Empty });
            return response.TemplateList.Convert();
        }

        public static void SendHealthCheckNotification(DateTime reportDate, string environment, string[] recipients, string classifiedsHtmlTableContent, string logHtmlTableContent = "")
        {
            const string healthCheckTemplateName = "SystemHealthCheck";
            const string dateFormat = "dd-MMM-yyyy";
            string subject = "Health Check report for " + reportDate.ToString(dateFormat);

            Collection<EmailRecipientView> emailRecipientViews = new Collection<EmailRecipientView>();
            foreach (string recipient in recipients)
            {
                emailRecipientViews.Add(new EmailRecipientView { Email = recipient });
            }

            Collection<TemplateItemView> templateItems = new Collection<TemplateItemView>
                {
                    new TemplateItemView("ClassifiedsTable", classifiedsHtmlTableContent), 
                    new TemplateItemView("LogTable", logHtmlTableContent),
                    new TemplateItemView("ReportDate", reportDate.ToString(dateFormat)),
                    new TemplateItemView("Environment", environment),
                };


            SendEmail(healthCheckTemplateName, emailRecipientViews, templateItems, subject);
        }
    }

}