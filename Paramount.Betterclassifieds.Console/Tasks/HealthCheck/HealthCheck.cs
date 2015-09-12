using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(SampleCall = "-to <commaDelimitedEmails>")]
    internal class HealthCheck : ITask
    {
        private readonly IBroadcastManager _broadcastManager;
        private readonly IApplicationConfig _applicationConfig;
        private string[] _emails;

        public HealthCheck(IBroadcastManager broadcastManager, IApplicationConfig applicationConfig)
        {
            _broadcastManager = broadcastManager;
            _applicationConfig = applicationConfig;
        }

        public void HandleArgs(TaskArguments args)
        {
            _emails = args.ReadArgument("to").Split(',');
        }

        public void Run()
        {
            var report = new ActivityReport
            {
                ReportDate = DateTime.Today.ToString("D"),
                Environment = _applicationConfig.Environment
            };

            var classifiedsResultsTask = Task<Dictionary<string, string>>.Factory.StartNew(GetClassifiedsStatistics);
            report.ClassifiedsTable = classifiedsResultsTask.Result.ToHtmlTable();

            var todaysLogsTask = Task<IEnumerable<LogItem>>.Factory.StartNew(GetTodaysLogs);
            report.LogTable = todaysLogsTask.Result.ToList().ToHtmlTable();

            // Block until all have completed
            Task.WaitAll(classifiedsResultsTask, todaysLogsTask);

            _broadcastManager.SendEmail(report, _emails);
        }

        public bool Singleton { get { return true; } }

        private IEnumerable<LogItem> GetTodaysLogs()
        {
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LogDbConnection"].ConnectionString))
            {
                var startDateUtc = DateTime.UtcNow.AddHours(-24);
                var endDateUtc = DateTime.UtcNow;

                var sql = "SELECT Application, Host, Type, Source, Message, User, StatusCode, TimeUtc FROM ELMAH_Error " +
                          "WHERE TimeUtc BETWEEN @StartDate AND @EndDate AND StatusCode != 404 " +
                          "ORDER BY Sequence";

                return connection.Query<LogItem>(sql, new { StartDate = startDateUtc.ToString("yyyy-MMM-dd"), EndDate = endDateUtc.ToString("yyyy-MMM-dd") });
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