using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.TaskScheduler
{
    class Program
    {
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
            ProcessJob(new[] { "SYSTEMHEALTHCHECKALERT/dejanvasic@outlook.com" });
            //ProcessJob(new[] {"EXPAD/1", "DAYSBEFOREEXPIRY/11"});
            //ProcessJob(new[] { "EMAILPROCESSING/" });
#else
            ProcessJob(args);
#endif

        }

        private static void ProcessJob(IEnumerable<string> args)
        {
            var parameters = new SchedulerParameters(args);
            var job = Jobs.FirstOrDefault(j => parameters.ContainsKey(j.Name.ToUpper()));
            if (job == null)
                return;

            try
            {
                job.Run(parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
