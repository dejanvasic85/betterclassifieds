using System;
using System.IO;
using System.Net;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    internal class CopyDb : ITask
    {
        private readonly ILogger _logger;
        private string _site;
        private DirectoryInfo _targetDir;
        private string _username;
        private string _password;
        private string[] _files;

        public CopyDb(ILogger logger)
        {
            _logger = logger;
        }

        public void HandleArgs(TaskArguments args)
        {
            _targetDir = new DirectoryInfo(args.ReadArgument("dir"));
            _files = args.ReadArgument("files").Split(',');
            _site = args.ReadArgument("site");

            if (!_targetDir.Exists)
                throw new ArgumentException($"The sourceDir {_targetDir} does not exist");

            _username = args.ReadArgument("username");
            _password = args.ReadArgument("password");
        }

        public void Run()
        {
            foreach (var file in _files)
            {
                DownloadFile(file);
            }
        }

        public bool Singleton => false;

        private void DownloadFile(string file)
        {
            _logger.Info("Downloading " + file);
            int tryCount = 1;
            while (tryCount < 5)
            {
                try
                {
                    var request = (FtpWebRequest)WebRequest.Create($"{_site}/{file}");
                    request.UsePassive = false;
                    request.Credentials = new NetworkCredential(_username, _password);
                    request.UseBinary = true;
                    request.KeepAlive = true;
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    var reader = request.GetResponse().GetResponseStream();

                    var fullPath = Path.Combine(_targetDir.FullName, file);
                    var fileStream = new FileStream(fullPath, FileMode.Create);
                    int bytesRead = 0;
                    byte[] buffer = new byte[2048];

                    while (true)
                    {
                        bytesRead = reader.Read(buffer, 0, buffer.Length);

                        if (bytesRead == 0)
                            break;

                        fileStream.Write(buffer, 0, bytesRead);

                        _logger.Progress();
                    }

                    fileStream.Close();
                    _logger.Info("Done");
                    break;
                }
                catch (Exception e)
                {
                    _logger.Error(e);

                    _logger.Info("Retrying... ");

                    tryCount = tryCount + 1;
                }
            }
        }
    }
}
