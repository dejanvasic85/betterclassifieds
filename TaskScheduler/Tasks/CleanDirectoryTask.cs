using System;
using System.IO;
using Paramount.ApplicationBlock.Logging.EventLogging;

namespace Paramount.Products.TaskScheduler
{
    public class CleanDirectoryTask:IScheduler 
    {
        //private const string DirectoryPath = "path";
        public void Run(SchedulerParameters parameters)
        {
            CleanupDirectory(parameters[Name]);
        }

        public string Name
        {
            get { return "CLEANDIRECTORY"; }
        }

        static void CleanupDirectory(string path)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(path);

                // Delete the directory and the contents
                if (directoryInfo.Exists)
                {
                    directoryInfo.Delete(true);
                }
            }
            catch (Exception ex)
            {
                // todo: Audit an error and send email to support (Paramount Devs)
                EventLogManager.Log(ex);
            }
        }
    }
}