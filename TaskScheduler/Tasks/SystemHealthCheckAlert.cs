namespace Paramount.TaskScheduler
{
    using System;
    using System.Linq;
    using ApplicationBlock.Configuration;

    using Common.DataTransferObjects.Betterclassifieds.Messages;
    using Common.DataTransferObjects.Broadcast.Messages;

    public class SystemHealthCheckAlert : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            // Fetch the classified data

            DateTime reportDate = DateTime.Today;
            var classifiedSummary = Services.Proxy.WebServiceHostManager
                .BetterclassifiedServiceClient
                .GetActivitySummary(new GetActivitySummaryRequest { ReportDate = reportDate })
                .ActivitySummary;


            // Fetch the broadcast data
            var broadcastSummary = Services.Proxy.WebServiceHostManager
                .BroadcastServiceHost
                .GetBroadcastActivity(new GetBroadcastActivityRequest { ReportDate = reportDate })
                .BroadcastActivitySummary;

            Broadcast.Components.EmailBroadcastController
                .SendHealthCheckNotification(
                    reportDate,
                    ConfigManager.ConfigurationContext,
                    parameters.First().Value.Split(';'),
                    classifiedSummary.NumberOfBookings,
                    classifiedSummary.SumOfBookings,
                    broadcastSummary.TotalNumberOfEmailsSent
                    );

        }

        public string Name
        {
            get { return "SYSTEMHEALTHCHECKALERT"; }
        }
    }
}