using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(Description = "Calls the broadcasting manager to send all un-processed notificaions")]
    internal class ProcessUnsentNotifications : ITask
    {
        public ProcessUnsentNotifications(IBroadcastManager broadcastManager)
        {
            broadcastManager.ProcessUnsent(100);
        }

        public void HandleArgs(TaskArguments args)
        {
        }
        
        public void Run()
        {
            // Runs the 

        }

        public bool Singleton { get { return true; } }
    }
}