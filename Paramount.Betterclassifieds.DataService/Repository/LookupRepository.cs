using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class LookupRepository : ILookupRepository
    {
        public List<string> GetLookupsForGroup(LookupGroup lookupGroup, string searchString = "")
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var values= context.Lookups
                    .Where(l => l.GroupName == lookupGroup.ToString())
                    .Select(a => a.LookupValue);

                if (StringExtensions.HasValue(searchString))
                    values = values.Where(lookup => lookup.StartsWith(searchString));

                return values.ToList();
            }
        }

        public void AddOrUpdate(LookupGroup group, string lookupValue)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Check if exists
                var lookup = context.Lookups.FirstOrDefault(l => l.GroupName == group.ToString() && l.LookupValue == lookupValue);
                if (lookup == null)
                {
                    context.Lookups.InsertOnSubmit(new Classifieds.Lookup { GroupName = group.ToString(), LookupValue = lookupValue });
                }
                context.SubmitChanges();
            }
        }
    }
}