using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EmailProcessor : INotificationProcessor
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly ISmtpMailer _mailer;
        
        public EmailProcessor(IBroadcastRepository broadcastRepository)
            : this(broadcastRepository, new DefaultMailer())
        {
            // Overloaded constructor
        }

        public EmailProcessor(IBroadcastRepository broadcastRepository, ISmtpMailer mailer)
        {
            _broadcastRepository = broadcastRepository;
            _mailer = mailer;
        }

        public bool Send(Guid broadcastId)
        {
            bool areAllComplete = true;
            
            // Fetch the email(s) per broadcast and process all of them (one by one)
            var emailsToRetry = _broadcastRepository.GetEmailsForNotification(broadcastId);

            foreach (var email in emailsToRetry)
            {
                // Ensure that we don't really need to retry
                if (email.ReachedMaxAttempts)
                    continue;

                email.Send(_mailer);
                _broadcastRepository.CreateOrUpdateEmail(email);
                
                // Try again
                if (!email.IsComplete)
                {
                    areAllComplete = false;
                }
            }

            return areAllComplete;
        }
    }
}