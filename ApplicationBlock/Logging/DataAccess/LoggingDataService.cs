using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Paramount.ApplicationBlock.Logging.DataAccess
{
    using System;
    using Logging.Enum;

    public class LoggingDataService
    {
        private const string ConfigSection = @"paramount/services";
        private const string ConfigKey = "ParamountLogConnection";

        public static void CreateLog(Guid? logId,
                                          string applicationName,
                                          string domain,
                                          string transactionName,
                                          string category,
                                          string userId,
                                          string accountId,
                                          string data1,
                                          string data2,
                                          DateTime? dateTimeCreated, string sessionId, string ipAddress, string hostName)
        {
            var dataFactory = new DataProxyFactory(Proc.LogInsert.Name, ConfigSection, ConfigKey);
            if (!logId.HasValue)
            {
                logId = Guid.NewGuid();
            }

            if (!dateTimeCreated.HasValue)
            {
                dateTimeCreated = DateTime.Now;
            }

            dataFactory.AddParameter(Proc.LogInsert.Params.LogId, logId.Value);
            dataFactory.AddParameter(Proc.LogInsert.Params.DateTimeCreated, dateTimeCreated.Value);
            dataFactory.AddParameter(Proc.LogInsert.Params.ApplicationName, applicationName, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.Domain, domain, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.TransactionName, transactionName, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.Category, category, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.User, userId, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.AccountId, accountId, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.Data1, data1, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.Data2, data2, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.SessionId, sessionId, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.IPAddress, ipAddress, StringType.VarChar);
            dataFactory.AddParameter(Proc.LogInsert.Params.HostName, hostName, StringType.VarChar);
            dataFactory.ExecuteNonQuery();
        }

        public static IEnumerable<EventLogSummary> GetEventLogSummary(DateTime reportDate)
        {
            var data = new DataProxyFactory("psp_ActivitySummary", ConfigSection, ConfigKey)
                .AddParameter("@ReportDate", reportDate)
                .ExecuteQuery()
                .Tables[0];

            return from DataRow row in data.Rows select new EventLogSummary(row);
        }
    }
}
