using Paramount.Betterclassifieds.Tests.Functional.Base;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
        public int AddAddress(object address)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Add(Constants.Table.Address, address);
            }
        }
    }
}
