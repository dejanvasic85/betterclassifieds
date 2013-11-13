using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class SpecialRate
    {
        public SpecialRate()
        {
            this.PublicationSpecialRates = new List<PublicationSpecialRate>();
        }

        public int SpecialRateId { get; set; }
        public Nullable<int> BaseRateId { get; set; }
        public Nullable<int> NumOfInsertions { get; set; }
        public Nullable<int> MaximumWords { get; set; }
        public Nullable<decimal> SetPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<int> NumOfAds { get; set; }
        public Nullable<bool> LineAdBoldHeader { get; set; }
        public Nullable<bool> LineAdImage { get; set; }
        public Nullable<int> NumberOfImages { get; set; }
        public virtual BaseRate BaseRate { get; set; }
        public virtual ICollection<PublicationSpecialRate> PublicationSpecialRates { get; set; }
    }
}
