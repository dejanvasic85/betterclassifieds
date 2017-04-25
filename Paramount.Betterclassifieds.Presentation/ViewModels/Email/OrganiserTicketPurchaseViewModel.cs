using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Email
{
    public class OrganiserTicketPurchaseViewModel
    {
        public string EventName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal TotalCost { get; set; }
        public int TotalTicketsPurchased { get; set; }
        public string EventDashboardUrl { get; set; }
    }
}