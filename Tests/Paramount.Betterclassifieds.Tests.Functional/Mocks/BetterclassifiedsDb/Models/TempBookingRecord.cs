using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb
{
    public partial class TempBookingRecord
    {
        public System.Guid BookingRecordId { get; set; }
        public decimal TotalCost { get; set; }
        public string SessionID { get; set; }
        public string UserId { get; set; }
        public System.DateTime DateTime { get; set; }
        public string AdReferenceId { get; set; }
    }
}
