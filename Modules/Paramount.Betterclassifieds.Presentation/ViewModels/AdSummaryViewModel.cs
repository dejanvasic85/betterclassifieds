
namespace Paramount.Betterclassifieds.Presentation.Models
{
    public class AdSummaryViewModel
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] ImageUrls { get; set; }
        public string CategoryName { get; set; }
        public string[] Publications { get; set; }

        public string TitleSlug
        {
            get { return Slug.Create(true, Title); }
        }
    }
}