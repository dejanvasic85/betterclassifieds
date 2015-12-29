using System;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface INotificationProcessor
    {
        /// <summary>
        /// Sends an email notification for the required document type and returns whether it has processed completely
        /// </summary>
        bool Send(Guid broadcastId);        
    }
}