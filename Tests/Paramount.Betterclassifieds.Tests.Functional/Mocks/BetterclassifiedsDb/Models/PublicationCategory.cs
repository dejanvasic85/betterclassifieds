using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class PublicationCategory
    {
        public PublicationCategory()
        {
            this.PublicationRates = new List<PublicationRate>();
            this.PublicationSpecialRates = new List<PublicationSpecialRate>();
        }

        public int PublicationCategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> MainCategoryId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public virtual MainCategory MainCategory { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual ICollection<PublicationRate> PublicationRates { get; set; }
        public virtual ICollection<PublicationSpecialRate> PublicationSpecialRates { get; set; }
    }
}
