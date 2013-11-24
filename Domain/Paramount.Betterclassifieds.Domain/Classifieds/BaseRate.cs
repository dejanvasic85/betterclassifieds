using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public class BaseRate
    {
        public BaseRate()
        {
            Ratecards = new List<Ratecard>();
        }

        public int BaseRateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<int> UpgradeBaseRateId { get; set; }
        public virtual ICollection<Ratecard> Ratecards { get; set; }
    }
}
