using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailProcessor : INotificationProcessor
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly ISmtpMailer _mailer;
        private readonly IApplicationConfig _config;
        
        public EmailProcessor(IBroadcastRepository broadcastRepository, IApplicationConfig config)
            : this(broadcastRepository, config, new DefaultMailer())
        {
            // Overloaded constructor
        }

        public EmailProcessor(IBroadcastRepository broadcastRepository, IApplicationConfig config, ISmtpMailer mailer)
        {
            _broadcastRepository = broadcastRepository;
            _mailer = mailer;
            _config = config;
        }

        /// <summary>
        /// Sends an email notification for the required document type and returns whether it has processed completely
        /// </summary>
        public bool Send<T>(T docType, Guid broadcastId, IDictionary<string, string> placeholderValues, params string[] to)
            where T : IDocType
        {
            // Template is required for emails, so fetch it
            var emailTemplate = _broadcastRepository.GetTemplateByName(docType.DocumentType, _config.Brand);

            var delivery = Email.BuildWithTemplate(docType, emailTemplate, broadcastId, to);

            return Send(delivery);
        }

        public bool Retry(Guid broadcastId)
        {
            bool areAllComplete = true;
            
            // Fetch the email(s) per broadcast and process all of them (one by one)
            var emailsToRetry = _broadcastRepository.GetEmailsForNotification(broadcastId);

            foreach (var email in emailsToRetry)
            {
                // Ensure that we don't really need to retry
                if (email.ReachedMaxAttempts)
                    continue;

                // Try again
                if (!Send(email))
                {
                    areAllComplete = false;
                }
            }

            return areAllComplete;
        }

        /// <summary>
        /// Returns true if the email has been sent or max attemps has reached
        /// </summary>
        private bool Send(Email email)
        {
            email.Send(_mailer);

            _broadcastRepository.CreateOrUpdateEmail(email);

            return email.IsComplete;
        }
    }
}