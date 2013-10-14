using System;
using Paramount.Broadcast.Components;

namespace Paramount.TaskScheduler
{
    public class EmailProcessing : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            EmailBroadcastController.ProcessEmailBroadcast(null);
        }

        public string Name
        {
            get { return "EMAILPROCESSING"; }
        }
    }
}