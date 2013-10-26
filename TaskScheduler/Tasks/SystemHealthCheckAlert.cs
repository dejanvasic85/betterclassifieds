using System.Threading.Tasks;
using Dapper;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Common.DataTransferObjects.Betterclassifieds;
using Paramount.Common.DataTransferObjects.Betterclassifieds.Messages;
using Paramount.Common.DataTransferObjects.Broadcast;
using Paramount.Common.DataTransferObjects.Broadcast.Messages;
using Paramount.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Paramount.TaskScheduler
{
    public class SystemHealthCheckAlert : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            var recipients = parameters.First().Value.Split(';');
            DateTime reportDate = DateTime.Today;

            var classifiedSummary = Task<ActivitySummary>.Factory.StartNew(() => 
                Services.Proxy.WebServiceHostManager.BetterclassifiedServiceClient
                    .GetActivitySummary(new GetActivitySummaryRequest { ReportDate = reportDate })
                    .ActivitySummary);

            var broadcastSummary = Task<BroadcastActivitySummary>.Factory.StartNew(() =>
                    Services.Proxy.WebServiceHostManager.BroadcastServiceHost
                        .GetBroadcastActivity(new GetBroadcastActivityRequest { ReportDate = reportDate })
                        .BroadcastActivitySummary
                );

            var todaysLogs = Task<IEnumerable<Models.LogItem>>.Factory.StartNew(GetTodaysLogs);

            // Block until all have completed
            Task.WaitAll(classifiedSummary, broadcastSummary, todaysLogs);

            // Fetch the system health check stuff
            Broadcast.Components.EmailBroadcastController
                .SendHealthCheckNotification(
                    reportDate,
                    ConfigManager.ConfigurationContext,
                    recipients,
                    classifiedSummary.Result.NumberOfBookings,
                    classifiedSummary.Result.SumOfBookings,
                    broadcastSummary.Result.TotalNumberOfEmailsSent,
                    todaysLogs.Result.ToList().ToHtmlTable()
                );
        }

        public string Name
        {
            get { return "SYSTEMHEALTHCHECKALERT"; }
        }

        private IEnumerable<Models.LogItem> GetTodaysLogs()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LogConnection"].ConnectionString))
            {
                var startDateUtc = DateTime.UtcNow.AddHours(-24);
                var endDateUtc = DateTime.UtcNow;

                return connection.Query<Models.LogItem>(
                    "SELECT Application, Host, Type, Source, Message, User, StatusCode, TimeUtc FROM ELMAH_Error WHERE TimeUtc BETWEEN @StartDate AND @EndDate ORDER BY Sequence", 
                    new { StartDate = startDateUtc, EndDate = endDateUtc }
                );
            }
        }
    }
}