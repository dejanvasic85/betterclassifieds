using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IMenuRepository
    {
        IDictionary<string, string> GetMenuItemLinkNamePairs();
        string GetFooterContent();
    }
}