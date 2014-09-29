using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class OnlineAdCart
    {
        public OnlineAdCart()
        {
            Images = new List<string>();
        }

        public string Heading { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public decimal? Price { get; set; }

        public int? LocationAreaId { get; set; }

        public List<string> Images { get; set; }

    }
}