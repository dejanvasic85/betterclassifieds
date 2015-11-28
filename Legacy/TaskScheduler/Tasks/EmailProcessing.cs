using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Broadcast;
using Paramount.Betterclassifieds.DataService.Repository;

namespace Paramount.TaskScheduler
{
    public class EmailProcessing : IScheduler
    {
        private readonly IBroadcastManager _broadcastManager;

        public EmailProcessing()
        {
            IBroadcastRepository broadcastRepository = new BroadcastRepository(new DbContextFactory());
            INotificationProcessor processor = new EmailProcessor(broadcastRepository, new AppConfig());

            _broadcastManager = new BroadcastManager(broadcastRepository, new[] { processor });
        }
        public void Run(SchedulerParameters parameters)
        {
            _broadcastManager.ProcessUnsent();
        }

        public string Name
        {
            get { return "EMAILPROCESSING"; }
        }
    }
}