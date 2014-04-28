using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    [Serializable]
    public class SearchFilters
    {
        [Display(Name = "Keyword or ID")]
        public string Keyword { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Sort By")]
        public int SortBy { get; set; } // This should map to the AdSearchSortOrder enum from the search business model

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        public int? AdId
        {
            get
            {
                int id;

                // Ensure that only a number was provided
                if (int.TryParse(Keyword, out id))
                {
                    return id;
                }

                // Ensure that publication id prefix format is supplied
                var regex = new Regex("^([0-9]*-[0-9]*){1}$");
                if (regex.IsMatch(Keyword))
                {
                    return int.Parse(Keyword.Split('-')[1]);
                }

                return null;
            }
        }

        public SearchFilters Clear()
        {
            // Clears all the current search filters
            Keyword = string.Empty;
            LocationId = CategoryId = null;
            return this;
        }

        public SearchFilters ApplySeoMapping(SeoNameMappingModel seoMapping)
        {
            this.Keyword = seoMapping.SearchTerm;
            this.CategoryId = seoMapping.ParentCategoryId;
            this.LocationId = seoMapping.LocationIds.FirstOrDefault();
            return this;
        }
    }
}