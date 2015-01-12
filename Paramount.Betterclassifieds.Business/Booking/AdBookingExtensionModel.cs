using System;

namespace Paramount.Betterclassifieds.Business.Booking
{
    public class AdBookingExtensionModel
    {
        public int AdBookingExtensionId { get; set; }
        public int AdBookingId { get; set; }
        public int Insertions { get; set; }
        public decimal ExtensionPrice { get; set; }
        public bool IsOnlineOnly { get; set; }
        public ExtensionStatus Status { get; set; }
        public string LastModifiedUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool IsFree
        {
            get { return this.ExtensionPrice == 0; }
        }

        public bool IsPaid
        {
            get { return !this.IsFree; }
        }
    }
}