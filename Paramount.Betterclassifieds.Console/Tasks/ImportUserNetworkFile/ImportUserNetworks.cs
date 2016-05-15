using System.IO;
using System.Linq;
using CsvHelper;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(Description = "Processes / Creates user network records found in the csv file.")]
    internal class ImportUserNetworks : ITask
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        private string _fullPath;
        private string _userId;

        public ImportUserNetworks(IUserManager userManager, ILogger logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public void HandleArgs(TaskArguments args)
        {
            _fullPath = args.ReadArgument("fileName");
            _userId = args.ReadArgument("userId");

        }

        public void Run()
        {
            using (var fileStream = new FileStream(_fullPath, FileMode.Open))
            using (var stream = new StreamReader(fileStream))
            {
                var csvReader = new CsvReader(stream);
                var records = csvReader.GetRecords<UserNetworkCsvModel>().Where(n => n.MeetsRequirements());
                foreach (var userNetworkInfo in records)
                {
                    _logger.Info($"Creating {userNetworkInfo.Email}");
                    _userManager.CreateUserNetwork(_userId,
                        userNetworkInfo.Email,
                        userNetworkInfo.FullName);
                }
            }
        }

        public bool Singleton => true;
    }
}
