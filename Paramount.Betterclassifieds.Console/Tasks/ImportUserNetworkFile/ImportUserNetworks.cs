using System.IO;
using System.Linq;
using System.Transactions;
using CsvHelper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(Description = "Processes / Creates user network records found in the csv file.")]
    internal class ImportUserNetworks : ITask
    {
        private readonly IUserManager _userManager;
        private readonly ILogger _logger;
        private readonly EventManager _eventManager;
        private string _fullPath;
        private string _userId;
        private int _eventId;

        public ImportUserNetworks(IUserManager userManager, ILogger logger, EventManager eventManager)
        {
            _userManager = userManager;
            _logger = logger;
            _eventManager = eventManager;
        }

        public void HandleArgs(TaskArguments args)
        {
            _fullPath = args.ReadArgument("fileName");
            _userId = args.ReadArgument("userId");
            _eventId = args.ReadArgument<int>("eventId");
        }

        public void Run()
        {
            using (var fileStream = new FileStream(_fullPath, FileMode.Open))
            using (var stream = new StreamReader(fileStream))
            {  
                var csvReader = new CsvReader(stream);
                var records = csvReader.GetRecords<UserNetworkCsvModel>().Where(n => n.MeetsRequirements());
                foreach (var record in records)
                {
                    _logger.Info($"Creating {record.Email}");
                    using (var scope = new TransactionScope())
                    {
                        var person = _userManager.CreateUserNetwork(_userId,
                            record.Email,
                            record.FullName);

                        // Create the invite
                        _eventManager.CreateInvitationForUserNetwork(_eventId, person.UserNetworkId.GetValueOrDefault());

                        scope.Complete();

                    }
                }
            }
        }

        public bool Singleton => true;
    }
}
