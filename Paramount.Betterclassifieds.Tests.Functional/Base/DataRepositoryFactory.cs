using Paramount.Betterclassifieds.Tests.Functional.Mocks;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class DataRepositoryFactory
    {
        public static ITestDataRepository Create(IConfig config)
        {
            return new DapperDataRepository(config);
        }
    }
}