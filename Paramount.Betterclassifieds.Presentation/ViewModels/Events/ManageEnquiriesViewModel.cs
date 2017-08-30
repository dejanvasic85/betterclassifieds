using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class ManageEnquiriesViewModel
    {
        public int AdId { get; set; }

        public List<AdEnquiryViewModel> Enquiries { get; set; }
    }
}