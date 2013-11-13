using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class Ad
    {
        public Ad()
        {
            this.AdBookings = new List<AdBooking>();
            this.AdDesigns = new List<AdDesign>();
        }

        public int AdId { get; set; }
        public string Title { get; set; }   
        public string Comments { get; set; }
        public Nullable<bool> UseAsTemplate { get; set; }
        public virtual ICollection<AdBooking> AdBookings { get; set; }
        public virtual ICollection<AdDesign> AdDesigns { get; set; }
    }
}
