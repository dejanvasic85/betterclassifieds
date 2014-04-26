namespace Paramount.Betterclassifieds.Business.Models
{
    //ONLY BECAUSE ONLINE AD IS NOT USUABLE
    public class OnlineListingModel
    {
        public int OnlineAdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public decimal Price { get; set; }
        public int LocationId { get; set; }
        public int LocationAreaId { get; set; }
        public int CategoryId { get; set; }
        public string ContactName { get; set; }
        public string ContactType { get; set; }
        public string ContactValue { get; set; }
        public int NumOfViews { get; set; }
        public string UserId { get; set; }
        public string BookReference { get; set; }
        public int AdBookingId { get; set; }

        public string DocumentID { get; set; }
        public string CategoryTitle { get; set; }
        public int AdId { get; set; }

        public string Publications { get; set; }

        public string DocumentIds { get; set; }
    }
}