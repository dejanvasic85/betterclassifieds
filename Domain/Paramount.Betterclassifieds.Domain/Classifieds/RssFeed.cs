using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public class RssFeed
    {
        public int OnlineAdId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string BookReference { get; set; }
        public string UserId { get; set; }
        public Nullable<int> MainCategoryId { get; set; }
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Heading { get; set; }
        public string HtmlText { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ContactName { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public string CategoryTitle { get; set; }
    }
}
