using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class Location
    {
        public Location()
        {
            this.LocationAreas = new List<LocationArea>();
            this.OnlineAds = new List<OnlineAd>();
        }

        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<LocationArea> LocationAreas { get; set; }
        public virtual ICollection<OnlineAd> OnlineAds { get; set; }
    }
}
