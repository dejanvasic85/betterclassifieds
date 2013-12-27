using System.Collections.Generic;
using System.Linq;
using BetterclassifiedsCore.DataModel;
using Paramount;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.Repository
{
    public class LookupRepository : ILookupRepository
    {
        public List<string> GetLookupsForGroup(LookupGroup lookupGroup, string searchString = "")
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

        public void AddOrUpdate(LookupGroup group, string lookupValue)
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