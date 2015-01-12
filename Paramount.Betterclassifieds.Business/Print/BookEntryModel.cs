using System;

namespace Paramount.Betterclassifieds.Business.Print
{
    public class BookEntryModel
    {
        public int PublicationId { get; set; }
        public DateTime EditionDate { get; set; }
        public decimal EditionAdPrice { get; set; }
        public int AdBookingId { get; set;}
        public int BaseRateId { get; set; }
        public string RateType { get; set; }
        public decimal PublicationPrice { get; set; }   
    }
}