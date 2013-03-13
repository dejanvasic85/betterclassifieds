namespace Paramount.Products.TaskScheduler
{
    using System;
    using System.Linq;
    using ApplicationBlock.Logging.EventLogging;
    using Common.DataTransferObjects.Betterclassifieds.Messages;

    public class SystemHealthCheckAlert : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            try
            {
                // Fetch the classified data
                
                var classifiedSummary = Services.Proxy.WebServiceHostManager
                    .BetterclassifiedServiceClient
                    .GetActivitySummary(new GetActivitySummaryRequest { ReportDate = DateTime.Today })
                    .ActivitySummary;

                Broadcast.Components.EmailBroadcastController
                    .SendHealthCheckNotification(
                        parameters.First().Value.Split(';'),
                        classifiedSummary.NumberOfBookings,
                        classifiedSummary.SumOfBookings);
            }
            catch (Exception ex)
            {
                EventLogManager.Log(ex);
            }
        }

        public string Name
        {
            get { return "SYSTEMHEALTHCHECKALERT"; }
        }
    }
}