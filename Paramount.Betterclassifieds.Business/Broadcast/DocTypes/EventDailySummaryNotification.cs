using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class EventDailySummaryNotification : IDocType
    {
        public string DocumentType => "EventDailySummary";
        public IList<EmailAttachment> Attachments { get; set; }

        [Placeholder("event-name")]
        public string EventName { get; set; }

        [Placeholder("todays-date")]
        public string TodaysDate { get; set; }

        [Placeholder("daily-ticket-count")]
        public string DailyTicketsCount { get; set; }

        [Placeholder("total-ticket-count")]
        public string TotalTicketsCount { get; set; }


        [Placeholder("daily-ticket-value")]
        public string DailyTicketsValue { get; set; }

        [Placeholder("total-ticket-value")]
        public string TotalTicketsValue { get; set; }
    }
}