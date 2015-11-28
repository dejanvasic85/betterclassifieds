namespace BetterClassified.UIController
{
    using System.Collections.Generic;
    using Paramount.Common.DataTransferObjects.Betterclassifieds;
    using ViewObjects;
    using System.Linq;
    public static class Converter
    {
        public static List<ExpiredAdView> Convert(this List<ExpiredAdEntity> list)
        {
            return list.Select(a => a.Convert()).ToList();
        }

        public static ExpiredAdView Convert(this ExpiredAdEntity adEntity)
        {
            return new ExpiredAdView
                       {
                           AdBookingId = adEntity.AdBookingId,
                           AdId = adEntity.AdId,
                           BookingEndDate = adEntity.BookingEndDate,
                           BookingReference = adEntity.BookingReference,
                           BookingStartDate = adEntity.BookingStartDate,
                           LastPrintInsertionDate = adEntity.LastPrintInsertionDate,
                           MainCategoryId = adEntity.MainCategoryId,
                           Username = adEntity.Username,
                           TotalPrice = adEntity.TotalPrice,
                           BookingDate = adEntity.BookingDate
                       };
        }
    }
  
}