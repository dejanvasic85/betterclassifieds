using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastRepository
    {
        // Notifications
        void CreateOrUpdateNotification(Notification notification);
        List<Notification> GetIncompleteNotifications(int takeAmount);

        // Templates
        EmailTemplate GetTemplateByName(string templateName, string brand);

        // Emails
        long CreateOrUpdateEmail(Email email);
        Email[] GetEmailsForNotification(Guid broadcastId);
        
    }
}