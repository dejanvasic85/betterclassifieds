using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface ISmtpMailer
    {
        void SendEmail(string subject, string body, string from, EmailAttachment[] attachments, params string[] to);
    }

    public class DefaultMailer : ISmtpMailer
    {
        public void SendEmail(string subject, string body, string from, EmailAttachment[] attachments, params string[] to)
        {
            var mailMessage = new MailMessage
            {
                IsBodyHtml = true,
                Body = body,
                Subject = subject
            };
            mailMessage.To.Add(string.Join(",", to));
            mailMessage.From = new MailAddress(from);
            List<MemoryStream> attachmentStreams = new List<MemoryStream>();
            if (attachments != null)
            {
                foreach (var emailAttachment in attachments)
                {
                    var stream = new MemoryStream(emailAttachment.Content);
                    mailMessage.Attachments.Add(new Attachment(stream, emailAttachment.FileName, emailAttachment.ContentType));
                    attachmentStreams.Add(stream);
                }
            }
            var client = new SmtpClient();
            client.Send(mailMessage);
            attachmentStreams.ForEach(s => s.Dispose());
        }
    }
}