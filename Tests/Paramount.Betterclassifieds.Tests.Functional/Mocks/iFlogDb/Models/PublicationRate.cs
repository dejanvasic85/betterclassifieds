using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class PublicationRate
    {
        public int PublicationRateId { get; set; }
        public Nullable<int> PublicationAdTypeId { get; set; }
        public Nullable<int> PublicationCategoryId { get; set; }
        public Nullable<int> RatecardId { get; set; }
        public virtual PublicationAdType PublicationAdType { get; set; }
        public virtual PublicationCategory PublicationCategory { get; set; }
        public virtual Ratecard Ratecard { get; set; }
    }
}
