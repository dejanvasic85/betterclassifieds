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
                                                        new ExpiredAdNotification()
                                                    };

        static void Main(string[] args)
        {
            var parameters = new SchedulerParameters(args);
            var groupingid = Guid.NewGuid().ToString();

            AuditLogManager.Log( new AuditLog(ConfigSettingReader.ApplicationName)
                            {
                                TransactionName = "Request.ScheduleTask",
                                Data = "Start Schedule Task",
                                SecondaryData = groupingid
                            });
           
            foreach (var job in Jobs)
            {
                if(parameters.ContainsKey(job.Name.ToUpper()))
                {
                    AuditLogManager.Log(new AuditLog(ConfigSettingReader.ApplicationName)
                    {
                        TransactionName = "Request.RunScheduleTask",
                        Data = job.Name,
                        SecondaryData = groupingid
                    });

                    job.Run(parameters);

                    AuditLogManager.Log(new AuditLog(ConfigSettingReader.ApplicationName)
                    {
                        TransactionName = "Response.RunScheduleTask",
                        Data = job.Name,
                        SecondaryData = groupingid
                    });
                }
                
            }

            AuditLogManager.Log(new AuditLog(ConfigSettingReader.ApplicationName)
            {
                TransactionName = "Response.ScheduleTask",
                Data = "End Schedule Job",
                SecondaryData = groupingid
            });
      
        }

       

        
    }
}
