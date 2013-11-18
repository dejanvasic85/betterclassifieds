using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class AdType
    {
        public AdType()
        {
            this.AdDesigns = new List<AdDesign>();
            this.PublicationAdTypes = new List<PublicationAdType>();
        }

        public int AdTypeId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<bool> PaperBased { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<bool> Active { get; set; }
        public virtual ICollection<AdDesign> AdDesigns { get; set; }
        public virtual ICollection<PublicationAdType> PublicationAdTypes { get; set; }
    }
}
