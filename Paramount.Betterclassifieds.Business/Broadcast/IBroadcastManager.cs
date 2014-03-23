using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public interface IBroadcastManager
    {
        Guid SendEmail<T>(T broadcast) where T : Broadcast;
    }

    /// <summary>
    /// Prepares and sends broadcasts using template management
    /// </summary>
    public class BroadcastManager : IBroadcastManager
    {
        private readonly IBroadcastRepository _broadcastRepository;
        private readonly IBroadcastTemplateParser _parser;
        private readonly IBroadcastSender[] _senders;

        public BroadcastManager(IBroadcastRepository broadcastRepository)
        {
            _broadcastRepository = broadcastRepository;
            
            // Register just a basic template parser ( same one as before )
            _parser = new BroadcastTemplateParser();

            // Registration of all the possible senders ( currently email will do :)
            _senders = new IBroadcastSender[] { new EmailSender(_parser) };
        }

        public Guid SendEmail<T>(T broadcast) where T : Broadcast
        {
            Guid broadcastId = new Guid();

            EmailTemplate template = _broadcastRepository.GetTemplateByName(broadcast.TemplateName);

            var result = _senders.OfType<EmailSender>().First().Send(broadcast, template);
            
            // Todo - save the result

            return broadcastId;
        }
    }
}