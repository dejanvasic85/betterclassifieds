using System.Collections.Generic;

// fkn dependency

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface ILookupRepository
    {
        List<string> GetLookupsForGroup(Models.LookupGroup lookupGroup, string searchString = "");
        void AddOrUpdate(Models.LookupGroup group, string lookupValue);
    }
}