namespace Paramount.Broadcast.Components
{
    using System;
    using System.Collections.ObjectModel;
    using Common.DataTransferObjects.Broadcast;
    using Common.DataTransferObjects.Broadcast.Messages;
    using System.Collections.Generic;
    using ApplicationBlock.Configuration;
    using ApplicationBlock.Logging.AuditLogging;
    using ApplicationBlock.Logging.Constants;
    using ApplicationBlock.Logging.EventLogging;
    using Services.Proxy;
    using Utility;

    public class EmailBroadcastController
    {
        public static void InsertUpdateTemplate(EmailTemplateView templateView, bool update)
        {
            var groupingId = Guid.NewGuid().ToString();

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

            var auditLog = new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(request),
                TransactionName = TransactionNames.SendEmailRequest
            };
            AuditLogManager.Log(auditLog);
            WebServiceHostManager.BroadcastServiceHost.InsertUpdateTemplate(request);

            AuditLogManager.Log(new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = "success",
                TransactionName = TransactionNames.SendEmailResponse
            });
        }

        public static EmailTemplateView GetTemplate(string templateName)
        {
            var groupingId = Guid.NewGuid().ToString();

            var request = new GetEmailTemplateRequest { TemplateName = templateName };

            var auditLog = new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(request),
                TransactionName = TransactionNames.SendEmailRequest
            };
            AuditLogManager.Log(auditLog);
            var response = WebServiceHostManager.BroadcastServiceHost.GetEmailTemplate(request);

            AuditLogManager.Log(new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(response),
                TransactionName = TransactionNames.SendEmailResponse
            });
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
            var groupingId = Guid.NewGuid().ToString();
            var auditLog = new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(request),
                TransactionName = TransactionNames.SendEmailRequest
            };
            AuditLogManager.Log(auditLog);
            var response = WebServiceHostManager.BroadcastServiceHost.SendMail(request);

            auditLog = new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(response),
                TransactionName = TransactionNames.SendEmailResponse
            };
            AuditLogManager.Log(auditLog);

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
            var groupingId = Guid.NewGuid().ToString();
            var auditLog = new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(request),
                TransactionName = TransactionNames.SendEmailRequest
            };
            AuditLogManager.Log(auditLog);
            var response = WebServiceHostManager.BroadcastServiceHost.SendNewsletter(request);

            auditLog = new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(response),
                TransactionName = TransactionNames.SendEmailResponse
            };
            AuditLogManager.Log(auditLog);

            return true;
        }

        public static void ProcessEmailBroadcast(Guid? broadcastId)
        {
            try
            {
                var request = new ProcessEmailsRequest { BroadcastId = broadcastId };
                var groupingId = Guid.NewGuid().ToString();
                var auditLog = new AuditLog
                {
                    AccountId = ConfigSettingReader.ClientCode,
                    SecondaryData = groupingId,
                    Data = XmlUtilities.SerializeObject(request),
                    TransactionName = TransactionNames.ProcessEmailRequest
                };
                AuditLogManager.Log(auditLog);
                WebServiceHostManager.BroadcastServiceHost.Process(request);
                auditLog = new AuditLog
                {
                    AccountId = ConfigSettingReader.ClientCode,
                    SecondaryData = groupingId,
                    Data = "Success",
                    TransactionName = TransactionNames.ProcessEmailResponse
                };
                AuditLogManager.Log(auditLog);
            }
            catch (Exception ex)
            {
                EventLogManager.Log(ex);
            }
        }

        public static IEnumerable<EmailTemplateView> GetTemplatesForEntity(string clientCode)
        {
            var request = new GetEmailTemplateListRequest { ClientCode = clientCode };
            var groupingId = Guid.NewGuid().ToString();
            AuditLogManager.Log(new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(request),
                TransactionName = TransactionNames.GetEmailTemplatesRequest
            });

            var response = WebServiceHostManager.BroadcastServiceHost.GetEmailTemplateListByClient(request);

            AuditLogManager.Log(new AuditLog
            {
                AccountId = ConfigSettingReader.ClientCode,
                SecondaryData = groupingId,
                Data = XmlUtilities.SerializeObject(response),
                TransactionName = TransactionNames.GetEmailTemplatesResponse
            });

            response.TemplateList.Insert(0, new EmailTemplate { Description = "[Select Template]", Name = string.Empty });
            return response.TemplateList.Convert();
        }

        public static void SendHealthCheckNotification(string[] recipients, int totalBookings, decimal sumOfBookings)
        {
            // Fetch all the required data from the sources


            // Setup the notification with all the template replacement data

        }
    }

}