﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class BroadcastRepository : IBroadcastRepository
    {
        public void CreateNotification(Notification notification)
        {
            using (var context = new BroadcastContext())
            {
                context.Notifications.Add(notification);
                context.SaveChanges();
            }
        }

        public void CreateOrUpdateNotification(Notification notification)
        {
            using (var context = new BroadcastContext())
            {
                if (context.Notifications.Any(n => n.BroadcastId == notification.BroadcastId))
                {
                    // Update
                    var oldValue = context.Notifications.Single(e => e.BroadcastId == notification.BroadcastId);
                    context.Entry(oldValue).CurrentValues.SetValues(notification);
                    context.Entry(oldValue).State = EntityState.Modified;
                }
                else
                {
                    // Create
                    context.Notifications.Add(notification);
                }
                context.SaveChanges();
            }
        }

        public List<Notification> GetIncompleteNotifications(int takeAmount)
        {
            using (var context = new BroadcastContext())
            {
                var notifications = context.Notifications
                    .Where(n => !n.IsComplete)
                    .Take(takeAmount)
                    .OrderBy(n => n.CreatedDate);
                    
                return notifications.ToList();
            }
        }

        public EmailTemplate GetTemplateByName(string templateName)
        {
            using (var context = new BroadcastContext())
            {
                return context.EmailTemplates.FirstOrDefault(t => t.DocType.Equals(templateName));
            }
        }

        public int CreateOrUpdateEmail(Email email)
        {
            using (var context = new BroadcastContext())
            {
                if (email.EmailDeliveryId == null || email.EmailDeliveryId == default(int))
                {
                    // Create
                    context.Emails.Add(email);
                }
                else
                {
                    // Update
                    var oldValue = context.Emails.Single(e => e.EmailDeliveryId == email.EmailDeliveryId);
                    context.Entry(oldValue).CurrentValues.SetValues(email);
                    context.Entry(oldValue).State = EntityState.Modified;
                }

                context.SaveChanges();

                return email.EmailDeliveryId.GetValueOrDefault();
            }
        }

        public List<Email> GetUnsentEmails(Guid broadcastId, int maxAttempts)
        {
            using (var context = new BroadcastContext())
            {
                var emails = context.Emails.Where(email => email.BroadcastId == broadcastId
                                              && email.Attempts < maxAttempts
                                              && email.SentDate == null);

                return emails.ToList();
            }
        }
    }
}