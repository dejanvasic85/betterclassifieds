using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class Step1View
    {

        public IEnumerable<PublicationSelectionView> Publications { get; set; }

        [Required]
        public int? CategoryId { get; set; }

        [Required]
        public int? SubCategoryId { get; set; }
    }
}

