using System;
using System.Collections.Generic;

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
            var emailTemplate = _broadcastRepository.GetTemplateByName(docType.DocumentType);

            var delivery = EmailDelivery.BuildWithTemplate(docType, emailTemplate, broadcastId, to);
            
            try
            {
                delivery.Send(_mailer);
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