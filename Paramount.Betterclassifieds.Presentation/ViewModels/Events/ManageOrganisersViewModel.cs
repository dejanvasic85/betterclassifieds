﻿using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class ManageOrganisersViewModel
    {
        public int AdId { get; set; }
        public int EventId { get; set; }
        public IEnumerable<EventOrganiserInviteViewModel> Invites { get; set; }

        public IEnumerable<EventOrganiserViewModel> Organisers { get; set; }
        public string OwnerEmail { get; set; }
    }

    public class EventOrganiserViewModel
    {
        public int EventOrganiserId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public Guid? InviteToken { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDateUtc { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

    }

    public class EventOrganiserInviteViewModel
    {
        public int EventId { get; set; }
        public string Email { get; set; }
    }

    public class EventOrganiserNotificationsViewModel
    {
        public EventOrganiserNotificationsViewModel()
        {
            
        }

        public EventOrganiserNotificationsViewModel(int adId, int eventId)
        {
            AdId = adId;
            EventId = eventId;
            SubscribeToPurchaseNotifications = true;
            SubscribeToDailyNotifications = true;
        }

        public int AdId { get; set; }
        public int EventId { get; set; }
        public bool? SubscribeToPurchaseNotifications { get; set; }
        public bool? SubscribeToDailyNotifications { get; set; }
    }
}