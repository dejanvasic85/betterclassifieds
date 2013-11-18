using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class WebAdSpace
    {
        public int WebAdSpaceId { get; set; }
        public string Title { get; set; }
        public Nullable<int> SettingID { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string AdLinkUrl { get; set; }
        public string AdTarget { get; set; }
        public Nullable<int> SpaceType { get; set; }
        public string ImageUrl { get; set; }
        public string DisplayText { get; set; }
        public string ToolTipText { get; set; }
        public Nullable<bool> Active { get; set; }
        public virtual WebAdSpaceSetting WebAdSpaceSetting { get; set; }
    }
}
