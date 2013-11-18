using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class Ratecard
    {
        public Ratecard()
        {
            this.PublicationRates = new List<PublicationRate>();
        }

        public int RatecardId { get; set; }
        public Nullable<int> BaseRateId { get; set; }
        public Nullable<decimal> MinCharge { get; set; }
        public Nullable<decimal> MaxCharge { get; set; }
        public Nullable<decimal> RatePerMeasureUnit { get; set; }
        public Nullable<int> MeasureUnitLimit { get; set; }
        public Nullable<decimal> PhotoCharge { get; set; }
        public Nullable<decimal> BoldHeading { get; set; }
        public Nullable<decimal> OnlineEditionBundle { get; set; }
        public Nullable<decimal> LineAdSuperBoldHeading { get; set; }
        public Nullable<decimal> LineAdColourHeading { get; set; }
        public Nullable<decimal> LineAdColourBorder { get; set; }
        public Nullable<decimal> LineAdColourBackground { get; set; }
        public Nullable<decimal> LineAdExtraImage { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
        public virtual BaseRate BaseRate { get; set; }
        public virtual ICollection<PublicationRate> PublicationRates { get; set; }
    }
}
