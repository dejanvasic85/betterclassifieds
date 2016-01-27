
using System;
using Humanizer;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdSummaryViewModel
    {
        public AdSummaryViewModel()
        {
            AdRouteName = "adRoute";
        }
        public int AdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string[] ImageUrls { get; set; }
        public string CategoryName { get; set; }
        public string CategoryAdType { get; set; }
        public string CategoryFontIcon { get; set; }
        public string ParentCategoryName { get; set; }
        public string[] Publications { get; set; }
        public string HeadingSlug { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime StartDate { get; set; }
        public string AdRouteName { get; set; }
        public string BookingDateFriendly
        {
            get
            {
                if (BookingDate.HasValue)
                {
                    // Booking date has a time component, but start date does not
                    // So if the dates are the same day, then use the booking date
                    if (BookingDate.Value.Day == StartDate.Day)
                        return BookingDate.Value.Humanize(utcDate: false);
                }
                return StartDate.Humanize(utcDate: false);
            }
        }
        public string PrimaryImage { get; set; }
    }
}