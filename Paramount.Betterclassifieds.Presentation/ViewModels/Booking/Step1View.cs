using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class Step1View
    {
        public Step1View()
        { }

        public Step1View(IEnumerable<CategorySearchResult> categories, IEnumerable<PublicationSelectionView> publications,
            int? selectedCategoryId, int? selectedSubCategoryId, int[] selectedPublications)
        {
            this.ParentCategoryOptions = categories.Where(c => c.ParentId == null).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() });
            this.Publications = publications;

            if (selectedCategoryId.HasValue)
            {
                var selectedParentCategory = categories.Single(c => c.MainCategoryId == selectedCategoryId);
                this.IsOnlineOnly = selectedParentCategory.IsOnlineOnly;
                this.CategoryId = selectedCategoryId;

                this.SubCategoryOptions = categories.Where(c => c.ParentId == selectedCategoryId).Select(c => new SelectListItem { Text = c.Title, Value = c.MainCategoryId.ToString() });
            }

            if (selectedSubCategoryId.HasValue)
            {
                var selectedSubCategory = categories.Single(c => c.MainCategoryId == selectedSubCategoryId);
                if (!IsOnlineOnly)
                {
                    this.IsOnlineOnly = selectedSubCategory.IsOnlineOnly;
                }
                this.SubCategoryId = selectedSubCategoryId;
            }

            this.SetSelectedPublications(selectedPublications);
        }

        public IEnumerable<SelectListItem> ParentCategoryOptions { get; set; }

        public IEnumerable<SelectListItem> SubCategoryOptions { get; set; }

        public IEnumerable<PublicationSelectionView> Publications { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public int? SubCategoryId { get; set; }

        public bool IsOnlineOnly { get; set; }

        public void SetSelectedPublications(int[] publications)
        {
            if (publications.IsNullOrEmpty() || Publications.IsNullOrEmpty())
                return;

            foreach (var selectedPublication in publications)
            {
                var publicationToSelect = Publications.SingleOrDefault(p => p.PublicationId == selectedPublication);

                if (publicationToSelect != null)
                {
                    publicationToSelect.IsSelected = true;
                }
            }
        }
    }
}

