using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastManager
    {
        Guid SendEmail<T>(T docType, params string[] to) where T : IDocType;
        void ProcessUnsent(int takeAmount = 10);
    }

    /// <summary>
    /// Prepares and sends broadcasts using template and subscription management
    /// </summary>
    public class BroadcastManager : IBroadcastManager
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly INotificationProcessor[] _processors;

        public BroadcastManager(IBroadcastRepository broadcastRepository, INotificationProcessor[] notificationProcessors)
        {
            _broadcastRepository = broadcastRepository;
            _processors = notificationProcessors;
        }

        public Guid SendEmail<T>(T docType, params string[] to) where T : IDocType
        {
            Notification notification = new Notification(Guid.NewGuid(), docType.DocumentType);

            _broadcastRepository.CreateOrUpdateNotification(notification);

            // Currently we only have an email processor - so just use that 
            // In the future, we'd like to have a bunch of processors 
            // that have a subscription system tied inside it ( per document type )
            var emailProcessor = _processors.OfType<EmailProcessor>().First();
            notification.IsComplete = emailProcessor.Send(docType, notification.BroadcastId, docType.ToPlaceholderDictionary(), to);

            // Persist back to repository
            _broadcastRepository.CreateOrUpdateNotification(notification);

            return notification.BroadcastId;
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
                        var task = Task<bool>.Factory.StartNew(() => processor.Retry(notification.BroadcastId));
                        results.Add(task.Result);
                        return task;
                    }).ToArray();
               
                // Wait until all of processors have completed
                Task.WaitAll(tasks.ToArray());

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