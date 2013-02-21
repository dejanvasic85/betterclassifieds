using System;
using Paramount.ApplicationBlock.Logging.EventLogging;

namespace Paramount.Products.TaskScheduler
{
    public class EmailProcessing:IScheduler 
    {
        public void Run(SchedulerParameters parameters)
        {
            try
            {
                Paramount.Broadcast.UIController.EmailBroadcastController.ProcessEmailBroadcast(null);
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