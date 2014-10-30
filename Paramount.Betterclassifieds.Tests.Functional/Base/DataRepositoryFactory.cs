using Paramount.Betterclassifieds.Tests.Functional.Mocks;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    public class DataRepositoryFactory
    {
        public static ITestDataRepository Create()
        {
            return new DapperDataRepository();
        }
    }
}