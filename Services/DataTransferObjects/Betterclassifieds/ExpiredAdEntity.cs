namespace Paramount.Common.DataTransferObjects.Betterclassifieds
{
    using System;

    public class ExpiredAdEntity
    {
        public int AdId { get; set; }
        public DateTime LastPrintInsertionDate { get; set; }
        public string Username { get; set; }
        public string BookingReference { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public int MainCategoryId { get; set; }
        public int AdBookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}