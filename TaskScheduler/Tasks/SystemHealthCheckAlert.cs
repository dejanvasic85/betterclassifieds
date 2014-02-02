using Dapper;
using Paramount.ApplicationBlock.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Paramount.TaskScheduler
{
    public class SystemHealthCheckAlert : IScheduler
    {
        public void Run(SchedulerParameters parameters)
        {
            var recipients = parameters.First().Value.Split(';');
            DateTime reportDate = DateTime.Today;
            
            var classifiedsResultsTask = Task<Dictionary<string, string>>.Factory.StartNew(GetClassifiedsStatistics);
            var classifiedResultsAsHtml = classifiedsResultsTask.Result.ToHtmlTable();
            
            var todaysLogsTask = Task<IEnumerable<Models.LogItem>>.Factory.StartNew(GetTodaysLogs);
            var todaysLogsAsHtml = todaysLogsTask.Result.ToList().ToHtmlTable();

            // Block until all have completed
            Task.WaitAll(classifiedsResultsTask, todaysLogsTask);

            // Use the built in broadcaster to send out the emails
            Broadcast.Components.EmailBroadcastController
                .SendHealthCheckNotification(
                    reportDate,
                    ConfigManager.ConfigurationContext,
                    recipients,
                    classifiedResultsAsHtml,
                    todaysLogsAsHtml
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

        private Dictionary<string, string> GetClassifiedsStatistics()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BetterclassifiedsConnection"].ConnectionString))
            {
                var dictionary = connection.Query("psp_Betterclassified_GetActivitySummary", commandType: CommandType.StoredProcedure)
                                           .ToDictionary(row => (string)row.StatisticName, row => (string)row.StatisticValue);

                return dictionary;
            }
        }
    }
}