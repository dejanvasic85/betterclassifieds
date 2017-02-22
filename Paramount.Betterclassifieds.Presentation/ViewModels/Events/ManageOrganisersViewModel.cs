using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class ManageOrganisersViewModel
    {
        public int AdId { get; set; }
        public int EventId { get; set; }
        public IEnumerable<EventOrganiserInviteViewModel> Invites { get; set; }

        public IEnumerable<EventOrganiserViewModel> Organisers { get; set; }
    }

    public class EventOrganiserViewModel
    {
        public int EventOrganiserId { get; set; }
        public int EventId { get; set; }
        public string UserId { get; set; }
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

    
}