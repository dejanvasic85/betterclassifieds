namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Business.Broadcast;

    public class BroadcastRepository : IBroadcastRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public BroadcastRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void CreateNotification(Notification notification)
        {
            using (var context = _dbContextFactory.CreateBroadcastContext())
            {
                context.Notifications.Add(notification);
                context.SaveChanges();
            }
        }

        public void CreateOrUpdateNotification(Notification notification)
        {
            using (var context = _dbContextFactory.CreateBroadcastContext())
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
            using (var context = _dbContextFactory.CreateBroadcastContext())
            {
                var notifications = context.Notifications
                    .Where(n => !n.IsComplete)
                    .Take(takeAmount)
                    .OrderBy(n => n.CreatedDate);

                return notifications.ToList();
            }
        }

        public EmailTemplate GetTemplateByName(string templateName, string brand)
        {
            using (var context = _dbContextFactory.CreateBroadcastContext())
            {
                return context.EmailTemplates.FirstOrDefault(t => t.DocType == templateName && t.Brand == brand);
            }
        }

        public long CreateOrUpdateEmail(Email email)
        {
            using (var context = _dbContextFactory.CreateBroadcastContext())
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

        public Email[] GetEmailsForNotification(Guid broadcastId)
        {
            using (var context = _dbContextFactory.CreateBroadcastContext())
            {
                return context
                    .Emails
                    .Where(email => email.BroadcastId == broadcastId)
                    .Where(email => email.SentDate == null)
                    .Include(email => email.EmailAttachments)
                    .ToArray();
            }
        }
    }
}
