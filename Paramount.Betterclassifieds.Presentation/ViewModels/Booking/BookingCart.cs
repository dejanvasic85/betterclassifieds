namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    /// <summary>
    /// View model representing what can be selected in a regular booking steps/stages
    /// </summary>
    public class BookingCart
    {
        public string SessionId { get; set; }

        public string Id { get; set; }

        public bool Completed { get; set; }

        public string UserId { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int[] Publications { get; set; }

        public string OnlineAdHeading { get; set; }

        public string OnlineAdDescription { get; set; }

        public string OnlineAdContactName { get; set; }

        public string OnlineAdPhone { get; set; }

        public string OnlineAdEmail { get; set; }

        public decimal? OnlineAdPrice { get; set; }

        public int? OnlineAdLocationAreaId { get; set; }

        public BookingCart()
        {
            Publications = new int[] { };
        }

        public bool IsStep1NotComplete()
        {
            return !CategoryId.HasValue || !SubCategoryId.HasValue;
        }
            
        public bool IsStep2NotComplete()
        {
            return this.OnlineAdHeading.IsNullOrEmpty() ||
                   this.OnlineAdDescription.IsNullOrEmpty();
        }
    }
}