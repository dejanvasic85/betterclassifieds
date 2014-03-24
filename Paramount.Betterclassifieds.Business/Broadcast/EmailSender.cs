using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Prepares broadcasts as MailMessage and sends it over smtp
    /// </summary>
    public class EmailSender
    {
        private readonly IBroadcastTemplateParser _parser;

        public EmailSender( IBroadcastTemplateParser parser )
        {
            _parser = parser;
        }
        
        public bool Send( EmailTemplate emailTemplate, IDictionary<string, string> tokens, string recipient )
        {
            var subject = _parser.ParseToString(emailTemplate.SubjectTemplate, tokens);
            var body = _parser.ParseToString(emailTemplate.BodyTemplate, tokens);

            // Prepare the actual email record
            MailMessage mailMessage = new MailMessage(emailTemplate.Sender,
                recipient, subject, body) { IsBodyHtml = true };

            // Todo - Store a record of the email

            try
            {
                SmtpClient client = new SmtpClient();
                client.Send( mailMessage );
                return true;
            }
            catch (Exception ex)
            {
                // Todo - store the exception message within the same record saved above ( to diagnose the last failure )
                return false;
            }
        }
    }
}