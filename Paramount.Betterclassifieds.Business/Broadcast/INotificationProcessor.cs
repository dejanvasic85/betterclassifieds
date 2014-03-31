using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public interface INotificationProcessor
    {
        /// <summary>
        /// Sends an email notification for the required document type and returns whether it has processed completely
        /// </summary>
        bool Send<T>(T docType, Guid broadcastId, IDictionary<string, string> placeholderValues, params string[] to)
            where T : IDocType;

        bool Retry(Guid broadcastId);
    }
}