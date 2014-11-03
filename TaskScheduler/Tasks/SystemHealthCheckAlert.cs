using Dapper;
using Paramount.ApplicationBlock.Configuration;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.DataService.Broadcast;
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
        private readonly IBroadcastManager _broadcastManager;

        public SystemHealthCheckAlert()
        {
            IBroadcastRepository broadcastRepository = new BroadcastRepository();
            INotificationProcessor processor = new EmailProcessor(broadcastRepository);

            _broadcastManager = new BroadcastManager(broadcastRepository, new[] { processor });
        }

        public void Run(SchedulerParameters parameters)
        {
            var report = new ActivityReport
            {
                ReportDate = DateTime.Today.ToString("D"),
                Environment = ConfigManager.ConfigurationContext
            };

            var classifiedsResultsTask = Task<Dictionary<string, string>>.Factory.StartNew(GetClassifiedsStatistics);
            report.ClassifiedsTable = classifiedsResultsTask.Result.ToHtmlTable();
            
            var todaysLogsTask = Task<IEnumerable<Models.LogItem>>.Factory.StartNew(GetTodaysLogs);
            report.LogTable = todaysLogsTask.Result.ToList().ToHtmlTable();

            // Block until all have completed
            Task.WaitAll(classifiedsResultsTask, todaysLogsTask);

            _broadcastManager.SendEmail(report, parameters.First().Value.Split(';'));
        }

        public string Name
        {
            get { return "SYSTEMHEALTHCHECKALERT"; }
        }

        private IEnumerable<Models.LogItem> GetTodaysLogs()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LogDbConnection"].ConnectionString))
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
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString))
            {
                var dictionary = connection.Query("psp_Betterclassified_GetActivitySummary", commandType: CommandType.StoredProcedure)
                                           .ToDictionary(row => (string)row.StatisticName, row => (string)row.StatisticValue);

                return dictionary;
            }
        }
    }
}