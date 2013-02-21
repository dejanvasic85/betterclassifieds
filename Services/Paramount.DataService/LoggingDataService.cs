namespace Paramount.DataService
{
    using System;
    using System.Data;
    using ApplicationBlock.Data;

    public class LoggingDataService
    {
        private const string ConfigSection = @"paramount/services";

        public static void CreateLog(string applicationName,
            string domain,
            string entityCode,
            string userId,
            string transactionName,
            string logCategory,
            string logMessage,
            string logStackTrace,
            string logException)
        {

        }

        public static string CreateAuditLog(Guid? logId, string applicationName, string domain, string transactionName,
            string category, string userId, string accountId, 
            string data1, string data2, DateTime? dateTimeCreated, 
            string sessionId, string ipAddress, string hostName, string browser)
        {
            var dataFactory = new DatabaseProxy(Proc.LogInsert.Name, ConfigSection, "LogConnection");
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
            dataFactory.AddParameter(Proc.LogInsert.Params.DateTimeUtcCreated, DateTime.UtcNow);
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
            dataFactory.AddParameter(Proc.LogInsert.Params.Browser, browser, StringType.VarChar);

            dataFactory.ExecuteNonQuery();

            return logId.ToString();
        }
    }
}
