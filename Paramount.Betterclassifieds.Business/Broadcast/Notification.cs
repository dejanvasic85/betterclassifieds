using System;

namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class Notification
    {
        protected Notification()
        {
            // Parameter-less constructor for Entity Framework (let's hope it works)
        }

        public Notification(Guid broadcastId, string docType = "")
        {
            this.BroadcastId = broadcastId;
            this.DocType = docType;
            this.CreatedDate = DateTime.Now;
            this.CreatedDateUtc = DateTime.UtcNow;
        }

        public Guid BroadcastId { get; private set; }
        public bool IsComplete { get; set; }
        public string DocType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedDateUtc { get; set; }
    }
}