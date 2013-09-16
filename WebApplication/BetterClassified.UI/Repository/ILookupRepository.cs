using System.Linq;
using BetterClassified.UI.Models;

namespace BetterClassified.UI.Repository
{
    public interface ILookupRepository
    {
        ILookup<string, string> GetLookups(LookupGroup lookupGroup);
    }

    public class LookupRepository : ILookupRepository
    {
        public ILookup<string, string> GetLookups(LookupGroup lookupGroup)
        {
            return null;
        }
    }
}