namespace Paramount.Products.TaskScheduler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ApplicationBlock.Configuration;
    using ApplicationBlock.Logging;
    using ApplicationBlock.Logging.EventLogging;
    using Common.DataTransferObjects.Betterclassifieds.Messages;
    using Common.DataTransferObjects.Broadcast.Messages;
    using Utility;

    public class SystemHealthCheckAlert : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            try
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

                // Fetch the log activities
                IEnumerable<EventLogSummary> eventLogSummaryList = EventLogManager.GetSummaryOfTopErrors(reportDate: reportDate);
                string htmlTableContent = eventLogSummaryList.ToList().ToHtmlTable();

                Broadcast.Components.EmailBroadcastController
                    .SendHealthCheckNotification(
                        reportDate,
                        ConfigManager.ConfigurationContext,
                        parameters.First().Value.Split(';'),
                        classifiedSummary.NumberOfBookings,
                        classifiedSummary.SumOfBookings,
                        broadcastSummary.TotalNumberOfEmailsSent,
                        htmlTableContent);
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