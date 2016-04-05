namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
        public int AddAddress(object address)
        {
            return _classifiedDb.Add(Constants.Table.Address, address);
        }
    }
}
