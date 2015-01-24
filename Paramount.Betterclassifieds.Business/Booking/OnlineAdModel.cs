using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public class OnlineAdModel : IAd
    {
        public OnlineAdModel()
        {
            Images = new List<AdImage>();
        }

        public int OnlineAdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public string MarkdownText { get; set; }
        public decimal Price { get; set; }
        public int LocationId { get; set; }
        public int LocationAreaId { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public int NumOfViews { get; private set; }

        public List<AdImage> Images { get; set; }

        public void IncrementHits()
        {
            this.NumOfViews++;
        }
    }
}