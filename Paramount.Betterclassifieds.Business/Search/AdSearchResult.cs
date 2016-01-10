using System;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Search
{
    public class AdSearchResult
    {
        public int AdId { get; set; }
        public int OnlineAdId { get; set; }
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
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Username { get; set; }
        public string[] ImageUrls { get; set; }
        public string[] Publications { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string LocationAreaName { get; set; }
        public int LocationAreaId { get; set; }
        public int TotalCount { get; set; }
        public string ParentCategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CategoryAdType { get; set; }

        public string PrimaryImage
        {
            get
            {
                if (this.ImageUrls == null)
                    return "placeholder";

                // For the moment we'll just return the first image
                return ImageUrls.FirstOrDefault();
            }
        }

        public bool HasExpired()
        {
            return EndDate < DateTime.Today;
        }

        public bool HasNotStarted()
        {
            return StartDate > DateTime.Today;
        }
    }
}