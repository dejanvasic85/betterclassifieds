using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    internal class TestEmailTemplate : ITask
    {
        private readonly IBroadcastManager _broadcastManager;

        public TestEmailTemplate(IBroadcastManager broadcastManager)
        {
            _broadcastManager = broadcastManager;
        }

        public void HandleArgs(TaskArguments args)
        {

        }

        public void Run()
        {
            _broadcastManager.SendEmail(new ForgottenPassword { Email = "dejanvasic@outlook.com" });
        }

        public bool Singleton { get; private set; }
    }
}