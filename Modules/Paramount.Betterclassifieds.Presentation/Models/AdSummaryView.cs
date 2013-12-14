using System;

namespace Paramount.Betterclassifieds.Presentation.Models
{
    public class AdSummaryView
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ParentCategoryName { get; set; }
        public string ChildCategoryName { get; set; }
        public DateTime PostedDate { get; set; }
    }
}