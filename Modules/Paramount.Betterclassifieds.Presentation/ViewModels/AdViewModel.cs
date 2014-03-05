using System;

namespace Paramount.Betterclassifieds.Presentation.Models
{
    public class AdViewModel
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ParentCategoryName { get; set; }
        public string ChildCategoryName { get; set; }
        public DateTime PostedDate { get; set; }
        public string[] Publications { get; set; }
        public string ContactName { get; set; }
        public string ContactDetail { get; set; }
        public int HitCount { get; set; }
    }
}