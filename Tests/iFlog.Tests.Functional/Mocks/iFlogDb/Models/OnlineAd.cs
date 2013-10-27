using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class OnlineAd
    {
        public OnlineAd()
        {
            this.OnlineAdEnquiries = new List<OnlineAdEnquiry>();
            this.TutorAds = new List<TutorAd>();
        }

        public int OnlineAdId { get; set; }
        public Nullable<int> AdDesignId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> LocationAreaId { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public Nullable<int> NumOfViews { get; set; }
        public string OnlineAdTag { get; set; }
        public virtual AdDesign AdDesign { get; set; }
        public virtual Location Location { get; set; }
        public virtual LocationArea LocationArea { get; set; }
        public virtual ICollection<OnlineAdEnquiry> OnlineAdEnquiries { get; set; }
        public virtual ICollection<TutorAd> TutorAds { get; set; }
    }
}
