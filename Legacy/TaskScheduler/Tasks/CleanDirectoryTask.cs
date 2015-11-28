using System;
using System.IO;

namespace Paramount.TaskScheduler
{
    public class CleanDirectoryTask : IScheduler
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
            var directoryInfo = new DirectoryInfo(path);

            // Delete the directory and the contents
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
        }
    }
}