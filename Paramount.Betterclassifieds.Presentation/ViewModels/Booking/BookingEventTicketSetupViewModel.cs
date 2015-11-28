﻿using System.Collections.Generic;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingEventTicketSetupViewModel
    {
        public List<BookingEventTicketViewModel> Tickets { get; set; }

        public List<EventTicketFieldViewModel> TicketFields { get; set; }
    }
}