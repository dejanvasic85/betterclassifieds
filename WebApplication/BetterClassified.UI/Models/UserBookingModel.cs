using System;

namespace BetterClassified.Models
{
    public class UserBookingModel
    {
        public const int ExpiringAdDaysBeforeToday = 7;

        public int AdBookingId { get; set; }
        public string BookingReference { get; set; }
        public string CategoryName { get; set; }
        public string AdTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OnlineImageId { get; set; }
        public string LineAdImageId { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? OnlineAdId { get; set; }
        public int? LineAdId { get; set; }
        
        public bool AboutToExpire
        {
            get { return EndDate.AddDays(-ExpiringAdDaysBeforeToday) < DateTime.Today && EndDate > DateTime.Today; }
        }

        public bool Expired
        {
            get { return EndDate < DateTime.Today; }
        }
        
        public bool IsPaid
        {
            get { return TotalPrice.GetValueOrDefault() > 0; }
        }

        public string ImageId
        {
            get 
            { 
                return OnlineImageId.HasValue() 
                    ? OnlineImageId 
                    : LineAdImageId.HasValue() 
                        ? LineAdImageId 
                        : string.Empty; 
            }
        }

        
    }
}