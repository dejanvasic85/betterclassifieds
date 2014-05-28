using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdViewModel
    {
        public AdViewModel()
        {
            AdEnquiry = new AdEnquiryViewModel();
        }
        public int AdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string HtmlText { get; set; }
        public String[] ImageUrls { get; set; }
        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }
        public DateTime PostedDate { get; set; }
        public string[] Publications { get; set; }
        public string ContactName { get; set; }
        public string ContactDetail { get; set; }
        public int NumOfViews { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool HasImages
        {
            get { return this.ImageUrls != null && this.ImageUrls.Length > 0; }
        }

        public bool ShowImageSlideshow
        {
            get { return this.HasImages && this.ImageUrls.Length > 1; }
        }

        public AdEnquiryViewModel AdEnquiry { get; set; }
    }
}