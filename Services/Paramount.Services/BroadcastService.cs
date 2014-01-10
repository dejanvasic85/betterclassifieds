using Paramount.Betterclassifieds.DataService;

namespace Paramount.Services
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
    using System.Net.Mail;
    using Common.ServiceContracts;
    using Common.DataTransferObjects.Broadcast;
    using Common.DataTransferObjects.Broadcast.Messages;

    public class BroadcastService : IBroadcastService
    {
        private const string EmailTemplateKey = "[/{0}/]";
        private const string BroadCastIdKey = "broadcastId";

        public SendEmailResponse SendEmail(SendEmailRequest request)
        {
            var templateRows = BroadcastDataService.GetEmailTemplateSelectByName(request.EmailTemplate);
            if (templateRows == null)
            {
                throw new Exception("Email Template not found");
            }

            var output = templateRows.EmailContent;
            var broadcastId = Guid.NewGuid();
            BroadcastDataService.EmailBroadcastInsert(broadcastId, templateRows.TemplateName, request.ClientCode,
                                                      request.ApplicationName, "Email");
            request.TemplateValue.Add(BroadCastIdKey, broadcastId.ToString());

            output = request.TemplateValue.Aggregate(output, (current, item) => current.Replace(GetEmailTemplateValue(item.Name), item.Value));
            foreach (var item in request.Recipients)
            {
                if (item.TemplateValue != null)
                    output = item.TemplateValue.Aggregate(output, (current, item2) => current.Replace(GetEmailTemplateValue(item2.Name), item2.Value));
                BroadcastDataService.EmailBroadcastEntryInsert(broadcastId, item.Email, output, string.IsNullOrEmpty(request.Subject) ? templateRows.Subject : request.Subject,
                                                               templateRows.Sender, request.IsBodyHtml,
                                                               (int)request.Priority);
            }

            return new SendEmailResponse { Success = true, BroadcastId = broadcastId };
        }

        public SendEmailResponse SendNewsletter(SendNewsLetterRequest request)
        {
            var templateRows = BroadcastDataService.GetEmailTemplateSelectByName(request.EmailTemplate);
            if (templateRows == null)
            {
                throw new Exception("Email Template not found");
            }

            var output = templateRows.EmailContent;
            var broadcastId = Guid.NewGuid();
            BroadcastDataService.EmailBroadcastInsert(broadcastId, templateRows.TemplateName, request.ClientCode,
                                                      request.ApplicationName, "Email");
            request.TemplateValue.Add(BroadCastIdKey, broadcastId.ToString());

            output = request.TemplateValue.Aggregate(output, (current, item) => current.Replace(GetEmailTemplateValue(item.Name), item.Value));
            foreach (var item in request.Recipients)
            {
                if (item.TemplateValue != null)
                    output = item.TemplateValue.Aggregate(output, (current, item2) => current.Replace(GetEmailTemplateValue(item2.Name), item2.Value));
                BroadcastDataService.EmailBroadcastEntryInsert(broadcastId, item.Email, output, string.IsNullOrEmpty(request.Subject) ? templateRows.Subject : request.Subject,
                                                               templateRows.Sender, request.IsBodyHtml,
                                                               (int)request.Priority);
            }

            return new SendEmailResponse { Success = true, BroadcastId = broadcastId };
        }

        private static string GetEmailTemplateValue(string key)
        {
            return string.Format(EmailTemplateKey, key);
        }

        internal void ValidateTemplate()
        {

        }

        public void ProcessEmails(ProcessEmailsRequest request)
        {
            var dataTable = request.BroadcastId.HasValue ? BroadcastDataService.GetUnsentEmailBroadcastEntry(request.BroadcastId.Value) : BroadcastDataService.GetUnsentEmailBroadcastEntry();

            foreach (var item in from DataRow row in dataTable.Rows select new EmailBroadcastEntryRow(row))
            {
                try
                {
                    SendEmail(item.Sender, item.Email, item.Subject, item.Body, item.Priority, item.IsBodyHtml);
                    BroadcastDataService.EmailBroadcastEntryProcess(item.EmailBroadcastEntryId, 1, DateTime.Now);
                }
                catch (Exception exception)
                {
                    BroadcastDataService.EmailBroadcastEntryProcess(item.EmailBroadcastEntryId, 1, null);
                    
                }
            }
        }

        public void CreateEmailBroadcastTrack(CreateEmailTrackRequest request)
        {
            BroadcastDataService.CreateEmailTrack(
                request.EmailBroadcastEntryId,
                request.ClientPage,
                request.IpAddress,
                request.Browser,
                request.Country,
                request.Region,
                request.City,
                request.Postcode,
                request.Latitude,
                request.Longitude,
                request.TimeZone
                );
        }

        public GetEmailTemplateListResponse GetEmailTemplateByEntity(GetEmailTemplateListRequest request)
        {
            var data = BroadcastDataService.SearchEmailTemplateByEntity(request.ClientCode);
            var list = new Collection<EmailTemplate>();

            foreach (DataRow row in data.Rows)
            {
                var item = new EmailTemplateRow(row);
                list.Add(new EmailTemplate
                {
                    Description = item.Description,
                    EmailContent = item.EmailContent,
                    Name = item.TemplateName,
                    Sender = item.Sender,
                    Subject = item.Subject,
                    ClientCode = item.EntityCode
                });
            }
            return new GetEmailTemplateListResponse
            {
                TemplateList = list
            };
        }

        public void InsertUpdateTemplate(InsertUpdateTemplateRequest request)
        {
            if (request.Update)
            {
                BroadcastDataService.UpdateEmailTemplate(request.ClientCode, request.Name, request.Description,
                                                         request.Subject, request.Sender, request.EmailContent);
            }
            else
            {
                BroadcastDataService.InsertEmailTemplate(request.ClientCode, request.Name, request.Description,
                                                        request.Subject, request.Sender, request.EmailContent);
            }

        }

        public GetEmailTemplateResponse GetEmailTemplate(GetEmailTemplateRequest emailTemplateRequest)
        {
            var templateRows = BroadcastDataService.GetEmailTemplateSelectByName(emailTemplateRequest.TemplateName);
            return new GetEmailTemplateResponse
            {
                Template = new EmailTemplate
                {
                    Description = templateRows.Description,
                    EmailContent = templateRows.EmailContent,
                    //EntityCode = templateRows.EntityCode,
                    Name = templateRows.TemplateName,
                    Sender = templateRows.Sender,
                    Subject = templateRows.Subject
                }
            };
        }

        public GetBroadcastActivityResponse GetBroadcastActivity(GetBroadcastActivityRequest request)
        {
            var activityRow = BroadcastDataService.GetBroadcastActivities(request.ReportDate).First();

            return new GetBroadcastActivityResponse
                {
                    BroadcastActivitySummary = new BroadcastActivitySummary
                        {
                            TotalNumberOfEmailsSent = activityRow.NumberOfEmailSent
                        }
                };
        }

        private static void SendEmail(string from, string to, string subject, string body, int priority, bool isBodyHtml)
        {
            var smtp = new SmtpClient();

            using (var message = new MailMessage(from, to, subject, body) { Priority = (MailPriority)priority, IsBodyHtml = isBodyHtml })
            {
                smtp.Send(message);
            }
        }

        public string GetServiceInfo()
        {
            return GetType().AssemblyQualifiedName;
        }
    }
}