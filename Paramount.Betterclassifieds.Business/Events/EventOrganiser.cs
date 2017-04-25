﻿using System;

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
        public bool? SubscribeToPurchaseNotifications { get; set; }
        public bool? SubscribeToDailyNotifications { get; set; }
        
        /// <summary>
        /// Returns whether this organiser is invited
        /// </summary>
        /// <returns></returns>
        public bool IsInvited()
        {
            return UserId.IsNullOrEmpty() && InviteToken.HasValue;
        }
    }

    public enum OrganiserConfirmationResult
    {
        NotFound,
        Success,
        AlreadyActivated,
        MismatchedEmail
    }

    public enum EmailSubscription
    {
        None,
        PerTicketPurchase,
        PerDay
    }
}
