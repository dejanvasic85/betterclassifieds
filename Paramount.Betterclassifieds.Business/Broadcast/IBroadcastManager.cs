using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastManager
    {
        Guid SendEmail<T>(T docType, params string[] to) where T : IDocType;
    }

    /// <summary>
    /// Prepares and sends broadcasts using template and subscription management
    /// </summary>
    public class BroadcastManager : IBroadcastManager
    {
        private readonly IBroadcastRepository _broadcastRepository;

        public BroadcastManager(IBroadcastRepository broadcastRepository)
        {
            _broadcastRepository = broadcastRepository;
        }

        public Guid SendEmail<T>(T docType, params string[] to) where T : IDocType
        {
            var broadcastGroupId = Guid.NewGuid();

            // Currently we only have an email processor
            var emailProcessor = new EmailProcessor(_broadcastRepository);
            emailProcessor.Send(docType, broadcastGroupId, docType.ToPlaceholderDictionary(), to);

            return broadcastGroupId;
        }
    }
}