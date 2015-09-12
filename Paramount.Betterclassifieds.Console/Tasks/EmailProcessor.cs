using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(SampleCall = "-take <maxEmailsToProcess>")]
    internal class EmailProcessor : ITask
    {
        private readonly IBroadcastManager _broadcastManager;

        private int _max;

        public EmailProcessor(IBroadcastManager broadcastManager)
        {
            _broadcastManager = broadcastManager;
        }

        public void HandleArgs(TaskArguments args)
        {
            _max = args.ReadArgument("take", false, () => 100);
        }
        
        public void Run()
        {
            // Runs the 
            _broadcastManager.ProcessUnsent(_max);
        }

        public bool Singleton { get { return true; } }
    }
}