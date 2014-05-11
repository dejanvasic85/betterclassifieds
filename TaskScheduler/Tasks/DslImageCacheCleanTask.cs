namespace Paramount.TaskScheduler
{
    using System;
    using System.IO;

    public class DslImageCacheCleanTask : IScheduler
    {
        private const int DaysToRetainImageCache = -1; // This is a good number because it's usually how long online ads run

        public void Run(SchedulerParameters parameters)
        {
            // Create directory info based on the first parameter passed in to the arg
            // e.g. IMAGECACHECLEAN/c:\Paramount\ImageCache\
            var directoryToClean = new DirectoryInfo(parameters[Name]);

            CleanDirectory(directoryToClean);
        }

        public string Name
        {
            get { return "IMAGECACHECLEAN"; }
        }
        
        static void CleanDirectory(DirectoryInfo dir)
        {
            // Clean this directory
            foreach (var file in dir.GetFiles())
            {
                var ts = DateTime.Now.Subtract(file.LastAccessTime);

                if (ts.Days <= DaysToRetainImageCache) continue;
                Console.WriteLine("Attempting to delete file " + file.FullName);
                try
                {
                    file.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed\nMessage: {0}\nStackTrace{1}", ex.Message, ex.StackTrace);
                }
            }

            // Make recursive call for all sub directories
            foreach (var childDirectory in dir.GetDirectories())
            {
                CleanDirectory(childDirectory);
            }
        }
    }
}