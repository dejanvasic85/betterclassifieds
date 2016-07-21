using System;
using System.IO;
using System.Text;
using Paramount.Betterclassifieds.Business.DocumentStorage;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help("-TaskName DownloadDocument -id 12345 -out console", Description = "Retrieves document from the database and writes to screen or file")]
    public class DownloadDocument : ITask
    {
        private readonly IDocumentRepository _repository;
        private readonly ILogger _logger;

        string Id { get; set; }
        string Output { get; set; }
        bool WriteToScreen { get; set; }
        
        public DownloadDocument(IDocumentRepository repository)
        {
            _repository = repository;
            _logger = new ConsoleLogger();
        }

        public void HandleArgs(TaskArguments args)
        {
            Id = args.ReadArgument("id", true);
            Output = args.ReadArgument("out");

            if (Output.Trim().ToLower().Equals("console"))
            {
                WriteToScreen = true;
            }
        }

        public void Run()
        {
            var doc = _repository.GetDocument(new Guid(Id));

            if (doc == null)
            {
                _logger.Warn("404 NOT FOUND");
                return;
            }

            if (WriteToScreen)
            {
                _logger.Info(doc.FileName);
                _logger.Info(doc.CreatedDate.ToString());
                _logger.Info(doc.Username);

                var str = doc.Data.FromByteArray(Encoding.ASCII);
                System.Console.WriteLine(str);
                return;
            }
            else
            {
                File.WriteAllBytes(Output, doc.Data);
            }
        }

        public bool Singleton => false;

    }
}
