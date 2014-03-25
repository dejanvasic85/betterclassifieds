using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    /// <summary>
    /// Prepares broadcasts as MailMessage and sends it over smtp
    /// </summary>
    public class EmailProcessor
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly ISmtpMailer _mailer;

        public EmailProcessor(IBroadcastRepository broadcastRepository)
            : this(broadcastRepository, new SmtpMailer())
        {
        }

        public EmailProcessor(IBroadcastRepository broadcastRepository, ISmtpMailer mailer)
        {
            _broadcastRepository = broadcastRepository;
            _mailer = mailer;
        }

        /// <summary>
        /// Sends an email notification for the required document type
        /// </summary>
        public bool Send<T>(T docType, Guid broadcastId, IDictionary<string, string> placeholderValues, params string[] to)
            where T : IDocType
        {
            bool result;

            // Template is required for emails, so fetch it
            EmailTemplate emailTemplate = _broadcastRepository.GetTemplateByName(docType.DocumentTemplate);

            // Fetch the parser required for this template ( by name )
            ITemplateParser parser = ParserFactory.ResolveParser(emailTemplate.ParserName);

            // Construct the body and subject by performing the parsing on each
            var subject = parser.ParseToString(emailTemplate.SubjectTemplate, placeholderValues);
            var body = parser.ParseToString(emailTemplate.BodyTemplate, placeholderValues);

            EmailDelivery delivery = new EmailDelivery(broadcastId, subject, body, emailTemplate.IsBodyHtml, emailTemplate.FromAddress, to);
            
            try
            {
                delivery.IncrementAttempts();
                _mailer.SendEmail(subject, body, emailTemplate.FromAddress, to);
                delivery.HasBeenSent();
                result = true;
            }
            catch (Exception ex)
            {
                delivery.LogException(ex);
                result = false;
            }
            finally
            {
                // Update the record
                _broadcastRepository.CreateOrUpdateEmail(delivery);
            }

            return result;
        }
    }
}