using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    [Obsolete]
    public interface IMenuRepository
    {
        IDictionary<string, string> GetMenuItemLinkNamePairs();
        string GetFooterContent();
    }
}