using System;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventOrganiser
    {
        public int EventOrganiserId { get; set; }
        public int EventId { get; set; }
        public EventModel Event { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public Guid? InviteToken { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Returns whether this organiser is invited
        /// </summary>
        /// <returns></returns>
        public bool IsInvited()
        {
            return UserId.IsNullOrEmpty() && InviteToken.HasValue;
        }
    }
}
