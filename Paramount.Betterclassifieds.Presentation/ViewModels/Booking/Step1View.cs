using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class Step1View
    {
        public BookingCart BookingCart { get; set; }

        public IEnumerable<SelectListItem> ParentCategories { get; set; }
        public IEnumerable<PublicationView> Publications { get; set; }
    }
}