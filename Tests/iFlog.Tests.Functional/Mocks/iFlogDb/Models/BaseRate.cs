using System;
using System.Collections.Generic;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    public partial class BaseRate
    {
        public BaseRate()
        {
            this.Ratecards = new List<Ratecard>();
            this.SpecialRates = new List<SpecialRate>();
        }

        public int BaseRateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> UpgradeBaseRateId { get; set; }
        public virtual ICollection<Ratecard> Ratecards { get; set; }
        public virtual ICollection<SpecialRate> SpecialRates { get; set; }
    }
}
