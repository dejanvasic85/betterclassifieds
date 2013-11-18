using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class DataSetup
    {
        private readonly ITestDataManager _dataManager;

        public DataSetup(ITestDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Given(@"The user with username ""(.*)"" does not exist")]
        public void GivenTheUserWithUsernameDoesNotExist(string username)
        {
            _dataManager.DropUserIfExists(username);
        }
    }
}