using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public interface IBroadcastManager
    {
        Guid SendEmail<T>(T broadcast) where T : Broadcast;
    }

    /// <summary>
    /// Prepares and sends broadcasts using template and subscription management
    /// </summary>
    public class BroadcastManager : IBroadcastManager
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly IBroadcastTemplateParser _templateParser;

        public BroadcastManager(IBroadcastRepository broadcastRepository, IBroadcastTemplateParser templateParser)
        {
            _broadcastRepository = broadcastRepository;
            _templateParser = templateParser;
        }

        public Guid SendEmail<T>(T broadcast) where T : Broadcast
        {
            Guid broadcastId = new Guid();

            EmailTemplate template = _broadcastRepository.GetTemplateByName(broadcast.TemplateName);

            EmailSender sender = new EmailSender(_templateParser);
            sender.Send(template, broadcast.GetPlaceholders(), broadcast.Recipient);
            
            // Todo - save the result

            return broadcastId;
        }
    }
}