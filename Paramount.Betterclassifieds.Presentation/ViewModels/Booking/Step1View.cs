using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class Step1View
    {
        public IEnumerable<SelectListItem> ParentCategoryOptions { get; set; }

        public IEnumerable<SelectListItem> SubCategoryOptions { get; set; }

        public IEnumerable<PublicationSelectionView> Publications { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public void SetSelectedPublications(int[] publications)
        {
            if (publications.IsNullOrEmpty() || Publications.IsNullOrEmpty())
                return;

            foreach (var selectedPublication in publications)
            {
               Publications.First(p => p.PublicationId == selectedPublication).IsSelected = true;
            }
        }
    }
}

