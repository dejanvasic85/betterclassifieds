using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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
        public string SortBy { get; set; }

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

    }
}