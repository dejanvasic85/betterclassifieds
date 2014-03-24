using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface IBroadcastManager
    {
        Guid Send<T>(T broadcast);
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

        public Guid Send<T>(T broadcast)
        {
            Guid broadcastId = Guid.NewGuid();

            return broadcastId;
        }
    }
}