using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class SuccessView
    {
        public bool IsBookingActive  { get; set; }
        public string AdId { get; set; }
        public string BookingID { get; set; }
        public List<string> ExistingUserContacts { get; set; }
        //use this in the view (for checkboxes) and later for db insertion?
        //public bool IsSelected { get; set; }
    }
}