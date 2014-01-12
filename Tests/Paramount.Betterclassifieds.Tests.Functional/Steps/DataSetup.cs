using OpenQA.Selenium;
using Paramount.Betterclassifieds.Tests.Functional.Mocks;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    public class DataSetup : BaseStep
    {
        private readonly ITestDataManager _dataManager;

        public DataSetup(IWebDriver driver, IConfig configuration,  ITestDataManager dataManager)
            : base(driver, configuration)
        {
            _dataManager = dataManager;
        }

        [Given(@"AdTypes ""(.*)"" and ""(.*)""")]
        public void GivenAdTypesAnd(string lineAdCode, string onlineAdCode)
        {
            _dataManager.AddAdTypeIfNotExists(lineAdCode);
            _dataManager.AddAdTypeIfNotExists(onlineAdCode);
        }

        [Given(@"The user with username ""(.*)"" does not exist")]
        public void GivenTheUserWithUsernameDoesNotExist(string username)
        {
            _dataManager.DropUserIfExists(username);
        }

        [Given(@"the publication ""(.*)"" has at least (.*) editions")]
        public void GivenThePublicationHasAtLeastEditions(string publicationName, int numberOfEditions)
        {
            _dataManager.AddEditionsToPublication(publicationName, numberOfEditions);
        }
    }
}