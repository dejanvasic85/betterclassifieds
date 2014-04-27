
namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AdSummaryViewModel
    {
        public int AdId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string[] ImageUrls { get; set; }
        public string CategoryName { get; set; }
        public string[] Publications { get; set; }
        public string HeadingSlug { get; set; }
    }
}