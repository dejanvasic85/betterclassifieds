using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class LocationArea
    {
        public LocationArea()
        {
            this.OnlineAds = new List<OnlineAd>();
        }

        public int LocationAreaId { get; set; }
        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<OnlineAd> OnlineAds { get; set; }
    }
}
