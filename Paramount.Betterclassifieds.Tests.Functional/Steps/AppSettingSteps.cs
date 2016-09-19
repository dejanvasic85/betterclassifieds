using Paramount.Betterclassifieds.Tests.Functional.Base;
using TechTalk.SpecFlow;

namespace Paramount.Betterclassifieds.Tests.Functional.Steps
{
    [Binding]
    internal class AppSettingSteps
    {
        private readonly ITestDataRepository _repository;

        public AppSettingSteps(ITestDataRepository repository)
        {
            _repository = repository;
        }

        [Given(@"setting ""(.*)"" is set to ""(.*)""")]
        public void GivenSettingIsSetToFalse(string settingName, string settingValue)
        {
            _repository.SetClientConfig(settingName, settingValue);
        }
    }
}
