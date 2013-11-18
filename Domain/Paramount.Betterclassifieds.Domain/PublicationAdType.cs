using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class PublicationAdType
    {
        public PublicationAdType()
        {
            this.PublicationRates = new List<PublicationRate>();
        }

        public int PublicationAdTypeId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public Nullable<int> AdTypeId { get; set; }
        public virtual AdType AdType { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual ICollection<PublicationRate> PublicationRates { get; set; }
    }
}
