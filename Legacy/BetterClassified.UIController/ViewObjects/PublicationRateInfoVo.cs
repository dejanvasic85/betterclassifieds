using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BetterclassifiedsCore.BusinessEntities;

namespace BetterClassified.UIController.ViewObjects
{
    public class PublicationRateInfoVo
    {
        public string PublicationName { get; set; }
        public int AdTypeId { get; set; }
        public string AdTypeCode { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal LineAdUnitAmount { get; set; }
        public int LineAdFreeUnits { get; set; }
        public decimal LineAdMainPhotoAmount { get; set; }
        public decimal LineAdHeaderAmount { get; set; }
        public decimal LineAdSuperBoldHeaderAmount { get; set; }
        public decimal LineAdColourBoldHeaderAmount { get; set; }
        public decimal LineAdColourBackgroundAmount { get; set; }
        public decimal LineAdColourBorderAmount { get; set; }
        public decimal LineAdFooterPhotoAmount { get; set; }

        public bool IsOnlineRate
        {
            get
            {
                return (BookingAdType)AdTypeId == BookingAdType.OnlineAd;
            }
        }

        public string OnlinePriceDescription
        {
            get
            {
                if ((BookingAdType)AdTypeId == BookingAdType.OnlineAd && MinimumAmount == 0)
                {
                    return "FREE";
                }
                else
                {
                    return MinimumAmount.ToString("C");
                }
            }
        }

        public bool IsPrintLineAdRate
        {
            get
            {
                return (BookingAdType)AdTypeId == BookingAdType.LineAd;
            }
        }

        public bool IsFirstWordsFree
        {
            get
            {
                return MinimumAmount == 0 && LineAdFreeUnits > 0;
            }
        }

        public bool MinimumAmountHasValue
        {
            get
            {
                return MinimumAmount > 0;
            }
        }

        public bool LineAdUnitAmountHasValue
        {
            get
            {
                return LineAdUnitAmount > 0;
            }
        }
    }
}
