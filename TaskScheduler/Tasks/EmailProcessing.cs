using System;
using Paramount.ApplicationBlock.Logging.EventLogging;
using Paramount.Broadcast.Components;

namespace Paramount.Products.TaskScheduler
{
    public class EmailProcessing : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            try
            {
                EmailBroadcastController.ProcessEmailBroadcast(null);
            }
            catch (Exception ex)
            {
                EventLogManager.Log(ex);
            }
        }

        public string Name
        {
            get { return "EMAILPROCESSING"; }
        }
    }
}