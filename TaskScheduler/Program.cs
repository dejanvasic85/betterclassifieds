using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Paramount.ApplicationBlock.Configuration;
using Paramount.ApplicationBlock.Logging.AuditLogging;

namespace Paramount.Products.TaskScheduler
{
    class Program
    {
        //Please add you job here
        private static readonly List<IScheduler> Jobs = new List<IScheduler>
                                                    {
                                                        new EmailProcessing(),
                                                        new CleanDirectoryTask(),
                                                        new DslImageCacheCleanTask(), 
                                                        new ExpiredAdNotification(),
                                                        new SystemHealthCheckAlert()
                                                    };

        static void Main(string[] args)
        {
#if DEBUG
            //ProcessJob(new[] { "SYSTEMHEALTHCHECKALERT/dejan.vasic@paramountit.com.au" });
            //ProcessJob(new[] {"EXPAD/1", "DAYSBEFOREEXPIRY/11"});
            ProcessJob(new[] { "EMAILPROCESSING/"});
#else
            ProcessJob(args);
#endif

        }

        private static void ProcessJob(IEnumerable<string> args)
        {
            var parameters = new SchedulerParameters(args);
            var groupingid = Guid.NewGuid().ToString();

            foreach (var job in Jobs.Where(job => parameters.ContainsKey(job.Name.ToUpper())))
            {
                LogTask(job, groupingid, "Request.RunScheduleTask");
                job.Run(parameters);
                LogTask(job, groupingid, "Response.RunScheduleTask");
            }
        }

        private static void LogTask(IScheduler job, string groupingid, string transactionName)
        {
            AuditLogManager.Log(
                new AuditLog(ConfigSettingReader.ApplicationName)
                    {
                        TransactionName = transactionName,
                        Data = job.Name,
                        SecondaryData = groupingid,
                        AccountId = ConfigSettingReader.ClientCode
                    });
        }
    }
}
