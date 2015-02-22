using System;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Print rate model
    /// </summary>
    public class RateModel
    {
        [Obsolete]
        public int BaseRateId { get; set; }
        public decimal? MinCharge { get; set; }
        public decimal? MaxCharge { get; set; }
        public decimal? RatePerWord { get; set; }
        public int FreeWords { get; set; }
        public decimal? PhotoCharge { get; set; }
        public decimal? BoldHeading { get; set; }
        public decimal? OnlineEditionBundle { get; set; }
        public decimal? LineAdSuperBoldHeading { get; set; }
        public decimal? LineAdColourHeading { get; set; }
        public decimal? LineAdColourBorder { get; set; }
        public decimal? LineAdColourBackground { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByUser { get; set; }

    }

}
