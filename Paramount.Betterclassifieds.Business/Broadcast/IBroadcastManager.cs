using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastManager
    {
        Guid SendEmail<T>(T broadcast, params string[] to) where T : IDocType;
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

        public Guid SendEmail<T>(T broadcast, params string[] to) where T : IDocType
        {
            Guid broadcastId = Guid.NewGuid();

            // Todo - create parent (grouping) record in Db

            // Currently we only have an email processor
            var emailProcessor = new EmailProcessor(_broadcastRepository);
            var result = emailProcessor.Send(broadcast, broadcastId, broadcast.ToPlaceholderDictionary(), to);

            // Todo - update the parent record success flag based on result

            return broadcastId;
        }
    }
}