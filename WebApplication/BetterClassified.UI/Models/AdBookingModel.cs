using System;

namespace BetterClassified.UI.Models
{
    public class AdBookingModel
    {
        public int AdBookingId { get; set; }
        public DateTime EndDate { get; set; }
        public BookingType BookingType { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }

        public bool IsExpired
        {
            get { return this.EndDate < DateTime.Today; }
        }
    }
}