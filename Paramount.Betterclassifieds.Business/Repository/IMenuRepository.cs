using System.Collections.Generic;

namespace BetterClassified.Repository
{
    public interface IMenuRepository
    {
        IDictionary<string, string> GetMenuItemLinkNamePairs();
        string GetFooterContent();
    }
}