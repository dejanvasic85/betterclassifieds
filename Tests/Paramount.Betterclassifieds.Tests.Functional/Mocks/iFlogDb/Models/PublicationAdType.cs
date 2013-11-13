using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class PublicationAdType
    {
        public PublicationAdType()
        {
            this.PublicationRates = new List<PublicationRate>();
            this.PublicationSpecialRates = new List<PublicationSpecialRate>();
        }

        public int PublicationAdTypeId { get; set; }
        public Nullable<int> PublicationId { get; set; }
        public Nullable<int> AdTypeId { get; set; }
        public virtual AdType AdType { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual ICollection<PublicationRate> PublicationRates { get; set; }
        public virtual ICollection<PublicationSpecialRate> PublicationSpecialRates { get; set; }
    }
}
