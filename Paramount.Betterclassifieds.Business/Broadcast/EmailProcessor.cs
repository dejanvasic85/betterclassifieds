using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailProcessor : INotificationProcessor
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly ISmtpMailer _mailer;
        private readonly int _maxAttempts;

        public EmailProcessor(IBroadcastRepository broadcastRepository)
            : this(broadcastRepository, new SmtpMailer())
        {
            // Overloaded constructor
        }

        public EmailProcessor(IBroadcastRepository broadcastRepository, ISmtpMailer mailer)
        {
            _broadcastRepository = broadcastRepository;
            _mailer = mailer;

            // Todo - read max attempts from a config provider
            _maxAttempts = 3;
        }

        /// <summary>
        /// Sends an email notification for the required document type and returns whether it has processed completely
        /// </summary>
        public bool Send<T>(T docType, Guid broadcastId, IDictionary<string, string> placeholderValues, params string[] to)
            where T : IDocType
        {
            // Template is required for emails, so fetch it
            var emailTemplate = _broadcastRepository.GetTemplateByName(docType.DocumentType);

            var delivery = Email.BuildWithTemplate(docType, emailTemplate, broadcastId, to);

            return Send(delivery);
        }

        public bool Retry(Guid broadcastId)
        {
            bool areAllComplete = true;
            
            // Fetch the email(s) per broadcast and process all of them (one by one)
            var emailsToRetry = _broadcastRepository.GetUnsentEmails(broadcastId, _maxAttempts);

            foreach (var email in emailsToRetry)
            {
                // Ensure that we don't really need to retry
                if (email.HasCompleted(_maxAttempts))
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

            return email.HasCompleted(_maxAttempts);
        }
    }
}