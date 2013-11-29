// fkn dependency
using System.Collections.Generic;
using Paramount.DomainModel.Business.Betterclassifieds.Enums;

namespace Paramount.DomainModel.Business.Repositories
{
    public interface ILookupRepository
    {
        List<string> GetLookupsForGroup(LookupGroup lookupGroup, string searchString = "");
        void AddOrUpdate(LookupGroup group, string lookupValue);
    }
}