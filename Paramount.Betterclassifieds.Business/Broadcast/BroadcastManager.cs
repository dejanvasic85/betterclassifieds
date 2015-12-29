using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastManager
    {
        /// <summary>
        /// Sends an email immediately
        /// </summary>
        Notification SendEmail<T>(T docType, params string[] to) where T : IDocType;

        /// <summary>
        /// Saves the notification to the database queue so that it's picked up by offline processor 
        /// </summary>
        Notification Queue<T>(T docType, params string[] to) where T : IDocType;

        /// <summary>
        /// Does the processing of all unsent emails in the queue
        /// </summary>
        void ProcessUnsent(int takeAmount = 10);
    }

    /// <summary>
    /// Prepares and sends broadcasts using template and subscription management
    /// </summary>
    public class BroadcastManager : IBroadcastManager
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly INotificationProcessor[] _processors;
        private readonly IApplicationConfig _applicationConfig;

        public BroadcastManager(IBroadcastRepository broadcastRepository, INotificationProcessor[] notificationProcessors, IApplicationConfig applicationConfig)
        {
            _broadcastRepository = broadcastRepository;
            _processors = notificationProcessors;
            _applicationConfig = applicationConfig;

            if (notificationProcessors == null || notificationProcessors.Length == 0)
            {
                throw new ArgumentNullException("notificationProcessors", "There are no notificationProcessors setup. Please check the unity config and try again.");
            }
        }

        public Notification SendEmail<T>(T docType, params string[] to) where T : IDocType
        {
            var notification = Queue(docType, to);

            // Currently we only have an email processor - so just use that 
            // In the future, we'd like to have a bunch of processors 
            // that have a subscription system tied inside it ( per document type )

            var emailProcessor = _processors.OfType<EmailProcessor>().First();
            notification.IsComplete = emailProcessor.Send(notification.BroadcastId);

            // Persist back to repository
            _broadcastRepository.CreateOrUpdateNotification(notification);

            return notification;
        }

        public Notification Queue<T>(T docType, params string[] to) where T : IDocType
        {
            Guard.NotNullOrEmptyIn(to);
            var notification = new Notification(Guid.NewGuid(), docType.DocumentType);

            _broadcastRepository.CreateOrUpdateNotification(notification);

            // Template is required for emails, so fetch it
            var emailTemplate = _broadcastRepository.GetTemplateByName(docType.DocumentType, _applicationConfig.Brand);
            var delivery = Email.BuildWithTemplate(docType, emailTemplate, notification.BroadcastId, to);
            _broadcastRepository.CreateOrUpdateEmail(delivery);

            return notification;
        }

        public void ProcessUnsent(int takeAmount = 10)
        {
            List<Notification> notificationsForProcessing = _broadcastRepository.GetIncompleteNotifications(takeAmount);

            foreach (var notification in notificationsForProcessing)
            {
                // Keep track results from all processors
                ConcurrentBag<bool> results = new ConcurrentBag<bool>();

                // Create a task for each processor
                var tasks = _processors
                    .Select(processor =>
                    {
                        var task = Task<bool>.Factory.StartNew(() => processor.Send(notification.BroadcastId));
                        results.Add(task.Result);
                        return task;
                    }).ToArray();

                // Wait until all of processors have completed
                Task.WaitAll(tasks);

                if (results.All(r => r))
                {
                    notification.IsComplete = true;
                }

                // Persist back to repository
                _broadcastRepository.CreateOrUpdateNotification(notification);
            }
        }
    }
}