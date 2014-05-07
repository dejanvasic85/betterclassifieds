using System;

namespace Paramount.Betterclassifieds.Business.Search
{
    public class AdSearchResult
    {
        public int AdId { get; set; }
        public string Heading { get; set; }
        public string HeadingSlug
        {
            get { return Slug.Create(true, Heading); }
        }

        public string Description { get; set; }
        public string HtmlText { get; set; }
        public decimal Price { get; set; }
        public DateTime? BookingDate { get; set; }
        public int NumOfViews { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public string Username { get; set; }
        public string[] ImageUrls { get; set; }
        public string[] Publications { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public int LocationId { get; set; }
        public string LocationTitle { get; set; }
        public string AreaTitle { get; set; }
        public int LocationAreaId { get; set; }
        public int TotalCount { get; set; }
    }
}