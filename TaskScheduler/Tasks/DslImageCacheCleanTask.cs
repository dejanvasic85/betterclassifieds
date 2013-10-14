namespace Paramount.TaskScheduler
{
    using System;
    using System.IO;

    public class DslImageCacheCleanTask : IScheduler
    {
        private const int DslFileDurationHours = 6;

        public void Run(SchedulerParameters parameters)
        {
            CleanUpDslImageCache(parameters[Name]);
        }

        public string Name
        {
            get { return "IMAGECACHECLEAN"; }
        }

        static void CleanUpDslImageCache(string path)
        {
            // Only perform clean of cache for files older than 24 hours
            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles();

            foreach (var file in files)
            {
                TimeSpan ts = DateTime.Now.Subtract(file.LastAccessTime);
                Console.WriteLine(ts.Hours.ToString());

                if (ts.TotalHours > DslFileDurationHours)
                {
                    Console.WriteLine("Deleting File " + file.FullName);
                    file.Delete();
                }
            }
        }
    }
}