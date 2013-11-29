using System;

namespace Paramount.DomainModel.Business.OnlineClassies.Models
{
    public interface IRateModel
    {
        int BaseRateId { get; set; }
        decimal? MinCharge { get; set; }
        decimal? MaxCharge { get; set; }
        decimal? RatePerWord { get; set; }
        int FreeWords { get; set; }
        decimal? PhotoCharge { get; set; }
        decimal? BoldHeading { get; set; }
        decimal? OnlineEditionBundle { get; set; }
        decimal? LineAdSuperBoldHeading { get; set; }
        decimal? LineAdColourHeading { get; set; }
        decimal? LineAdColourBorder { get; set; }
        decimal? LineAdColourBackground { get; set; }
        DateTime? CreatedDate { get; set; }
        string CreatedByUser { get; set; }
    }
}