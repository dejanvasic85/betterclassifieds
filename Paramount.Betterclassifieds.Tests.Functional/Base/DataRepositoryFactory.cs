using Paramount.Betterclassifieds.Tests.Functional.Mocks;

namespace Paramount.Betterclassifieds.Tests.Functional
{
    internal class DataRepositoryFactory
    {
        public static ITestDataRepository Create(IConfig config)
        {
            var connectionFactory= new ConnectionFactory(config);
            return new DapperDataRepository(config, connectionFactory);
        }
    }
}