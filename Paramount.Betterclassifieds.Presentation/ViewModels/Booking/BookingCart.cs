using System;
using System.Linq;

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

        public DateTime? StartDate { get; set; }

        public OnlineAdCart OnlineAdCart { get; set; }

        public BookingCart()
        {
            Publications = new int[] { };
        }

        public bool IsLineAdIncluded
        {
            get { return this.Publications != null && this.Publications.Any(); }
        }

        public bool IsStep1NotComplete()
        {
            return !CategoryId.HasValue || !SubCategoryId.HasValue;
        }

        public bool IsStep2NotComplete()
        {
            return this.OnlineAdCart != null &&
                    (this.OnlineAdCart.Heading.IsNullOrEmpty() ||
                   this.OnlineAdCart.Description.IsNullOrEmpty());
        }

        public bool IsStep3NotComplete()
        {
            return !this.StartDate.HasValue;
        }
    }
}