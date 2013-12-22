using System.Linq;
using System.Collections.Generic;

using BetterclassifiedsCore.DataModel;
using Paramount;

// fkn dependency

namespace BetterClassified.Repository
{
    public interface ILookupRepository
    {
        List<string> GetLookupsForGroup(Models.LookupGroup lookupGroup, string searchString = "");
        void AddOrUpdate(Models.LookupGroup group, string lookupValue);
    }

    public class LookupRepository : ILookupRepository
    {
        public List<string> GetLookupsForGroup(Models.LookupGroup lookupGroup, string searchString = "")
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                var values= context.Lookups
                              .Where(l => l.GroupName == lookupGroup.ToString())
                              .Select(a => a.LookupValue);

                if (searchString.HasValue())
                    values = values.Where(lookup => lookup.StartsWith(searchString));

                return values.ToList();
            }
        }

        public void AddOrUpdate(Models.LookupGroup group, string lookupValue)
        {
            using (var context = BetterclassifiedsDataContext.NewContext())
            {
                // Check if exists
                var lookup = context.Lookups.FirstOrDefault(l => l.GroupName == group.ToString() && l.LookupValue == lookupValue);
                if (lookup == null)
                {
                    context.Lookups.InsertOnSubmit(new Lookup {GroupName = group.ToString(), LookupValue = lookupValue});
                }
                context.SubmitChanges();
            }
        }
    }
}