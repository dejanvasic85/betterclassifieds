using System;
using System.Collections.Generic;
using System.Net.Configuration;
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
        public bool Send<T>( T docType, Guid broadcastId, IDictionary<string, string> placeholderValues, string to )
            where T : IDocType
        {
            // Template is required for emails, so fetch it
            EmailTemplate emailTemplate = _broadcastRepository.GetTemplateByName(docType.DocumentTemplate);

            // Fetch the parser required for this template
            ITemplateParser parser = ParserFactory.ResolveParser(emailTemplate.ParserName);

            // Construct the body and subject by performing the parsing on each
            var subject = parser.ParseToString(emailTemplate.SubjectTemplate, placeholderValues);
            var body = parser.ParseToString(emailTemplate.BodyTemplate, placeholderValues);

            _mailer.SendEmail( subject, body, emailTemplate.FromAddress, to );

            return true;
        }
    }
}