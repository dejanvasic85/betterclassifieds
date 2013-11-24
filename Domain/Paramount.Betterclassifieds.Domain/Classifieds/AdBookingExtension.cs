using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Domain
{
    public partial class AdBookingExtension
    {
        public int AdBookingExtensionId { get; set; }
        public int AdBookingId { get; set; }
        public Nullable<int> Insertions { get; set; }
        public Nullable<decimal> ExtensionPrice { get; set; }
        public int Status { get; set; }
        public string LastModifiedUserId { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
    }
}
