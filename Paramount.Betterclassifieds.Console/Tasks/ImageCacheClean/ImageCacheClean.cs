using System;
using System.IO;
using System.Linq;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(SampleCall = "-directory <directoryToClean>", Description = "Removes all images from a folder cache that are older than 1 month")]
    internal class ImageCacheClean : ITask
    {
        private readonly IDateService _dateService;
        private readonly ILogger _logger;

        private DirectoryInfo _directory;
        private bool _recurse;

        public ImageCacheClean(IDateService dateService, ILogger logger)
        {
            _dateService = dateService;
            _logger = logger;
        }

        public void HandleArgs(TaskArguments args)
        {
            var path = args.ReadArgument("directory", isRequired: true);
            if (!Directory.Exists(path))
            {
                throw new Exception("There is no such directory");
            }

            _directory = new DirectoryInfo(path);
            _recurse = args.ReadArgument("recurse", readDefault: () => false);
        }

        public void Run()
        {
            var filesToDelete = _directory.GetFiles().Where(file => (_dateService.Today - file.LastWriteTime.Date).TotalDays > 30);

            foreach (var file in filesToDelete)
            {
                try
                {
                    _logger.Info("Deleting file " + file.FullName);
                    file.Delete();
                }
                catch (Exception ex)
                {
                    _logger.Warn("Unable to clean.");
                    _logger.Warn(ex.Message);
                    ex.ToEventLog();
                }
            }
        }

        public bool Singleton { get { return true; } }
    }
}