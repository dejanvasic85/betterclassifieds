using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class SearchFilters
    {
        [Display(Name = "Keyword or ID")]
        public string Keyword { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        
        [Display(Name = "Sort By")]
        public string SortBy { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }
       
    }
}