using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Booking
{
    public class BookingEventTicketSetupViewModel
    {
        public List<BookingEventTicketViewModel> Tickets { get; set; }

        public List<EventTicketFieldViewModel> TicketFields { get; set; }

        public DateTime? ClosingDate { get; set; }

        public DateTime? AdStartDate { get; set; }

        public decimal EventTicketFee { get; set; }
        public decimal EventTicketFeeCents { get; set; }
        public bool? IncludeTransactionFee { get; set; }
    }
}