using System;
using System.Net.Mail;

namespace Paramount.Betterclassifieds.Business
{
    public interface IBroadcastSender
    {
        bool Send<T>(Broadcast broadcast, T template);
    }

    /// <summary>
    /// Sends broadcast objects over smtp client
    /// </summary>
    public class EmailSender : IBroadcastSender
    {
        private readonly IBroadcastTemplateParser _parser;

        public EmailSender( IBroadcastTemplateParser parser )
        {
            _parser = parser;
        }
        
        public bool Send<T>(Broadcast broadcast, T template)
        {
            if (typeof (T) != typeof (EmailTemplate))
            {
                throw new ArgumentException("Email Sender can only process EmailTemplates");
            }

            EmailTemplate emailTemplate = template as EmailTemplate;

            var tokens = broadcast.GetPlaceholders();
            var subject = _parser.ParseToString(emailTemplate.SubjectTemplate, tokens);
            var body = _parser.ParseToString(emailTemplate.BodyTemplate, tokens);

            // Prepare the email
            MailMessage mailMessage = new MailMessage(emailTemplate.Sender,
                broadcast.To, subject, body) { IsBodyHtml = true };

            // Todo - Store a record of the email

            try
            {
                SmtpClient client = new SmtpClient();
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // Todo - store the exception message ( to diagnose the last failure )
                return false;
            }
        }
    }
}