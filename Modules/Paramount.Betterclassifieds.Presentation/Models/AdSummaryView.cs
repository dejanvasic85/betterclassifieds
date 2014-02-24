
namespace Paramount.Betterclassifieds.Presentation.Models
{
    public class AdSummaryModel
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ParentCategoryName { get; set; }
        public string[] Publications { get; set; }
    }
}