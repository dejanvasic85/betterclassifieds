using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class OnlineAd1
    {
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
        public string UserId { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string BookReference { get; set; }
        public Nullable<int> BookingStatus { get; set; }
        public Nullable<int> MainCategoryId { get; set; }
        public Nullable<int> Insertions { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public string BookingType { get; set; }
        public int AdBookingId { get; set; }
        public string Category { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string Title { get; set; }
        public string Area { get; set; }
        public int AdId { get; set; }
    }
}
