using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class Step1View
    {
        public IEnumerable<SelectListItem> ParentCategoryOptions { get; set; }

        public IEnumerable<SelectListItem> SubCategoryOptions { get; set; } 

        public IEnumerable<PublicationView> Publications { get; set; }

        public int? SelectedCategoryId { get; set; }

        public int? SelectedSubCategoryId { get; set; }

        public int[] SelectedPublications { get; set; }
    }
}

