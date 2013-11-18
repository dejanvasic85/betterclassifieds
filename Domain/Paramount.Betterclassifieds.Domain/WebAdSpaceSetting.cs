using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class WebAdSpaceSetting
    {
        public WebAdSpaceSetting()
        {
            this.WebAdSpaces = new List<WebAdSpace>();
        }

        public int SettingId { get; set; }
        public string Title { get; set; }
        public int LocationCode { get; set; }
        public virtual ICollection<WebAdSpace> WebAdSpaces { get; set; }
    }
}
