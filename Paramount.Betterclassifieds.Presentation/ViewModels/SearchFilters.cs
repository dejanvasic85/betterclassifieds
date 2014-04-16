using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class SearchFilters
    {
        public string Keyword { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
    }
}