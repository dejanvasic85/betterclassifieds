using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public interface IMenuRepository
    {
        IDictionary<string, string> GetMenuItemLinkNamePairs();
        string GetFooterContent();
    }
}