using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventDailySummary : IDocType
    {
        public string DocumentType => GetType().Name;
        public IList<EmailAttachment> Attachments { get; set; }

        [Placeholder("event-name")]
        public string EventName { get; set; }


        [Placeholder("daily-guest-count")]
        public string DailyGuestCount { get; set; }


        [Placeholder("daily-tickets-count")]
        public string DailyTicketsCount { get; set; }


        [Placeholder("daily-tickets-value")]
        public string DailyTicketsValue { get; set; }
    }
}