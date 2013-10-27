using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class AdDesign
    {
        public AdDesign()
        {
            this.AdGraphics = new List<AdGraphic>();
            this.LineAds = new List<LineAd>();
            this.OnlineAds = new List<OnlineAd>();
        }

        public int AdDesignId { get; set; }
        public Nullable<int> AdId { get; set; }
        public Nullable<int> AdTypeId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> Version { get; set; }
        public Nullable<int> FirstAdDesignId { get; set; }
        public virtual Ad Ad { get; set; }
        public virtual AdType AdType { get; set; }
        public virtual ICollection<AdGraphic> AdGraphics { get; set; }
        public virtual ICollection<LineAd> LineAds { get; set; }
        public virtual ICollection<OnlineAd> OnlineAds { get; set; }
    }
}
