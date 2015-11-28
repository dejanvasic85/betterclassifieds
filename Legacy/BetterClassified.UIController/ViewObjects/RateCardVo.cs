using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified.UIController.ViewObjects
{
    public class RateCardVo
    {
        public int? RateCardId { get; set; }
        public string RateCardName { get; set; }
        public decimal MinCharge { get; set; }
        public decimal? MaxCharge { get; set; }
        public decimal? RatePerMeasureUnit { get; set; }
        public int? MeasureUnitLimit { get; set; }
        public decimal? PhotoCharge { get; set; }
        public decimal? BoldHeading { get; set; }
        public decimal? OnlineEditionBundle { get; set; }
        public decimal? LineAdSuperBoldHeading { get; set; }
        public decimal? LineAdColourHeading { get; set; }
        public decimal? LineAdColourBorder { get; set; }
        public decimal? LineAdColourBackground { get; set; }
        public decimal? LineAdExtraImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByUser { get; set; }
    }
}