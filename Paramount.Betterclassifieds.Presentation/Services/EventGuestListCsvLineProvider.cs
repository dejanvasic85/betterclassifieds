using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;
using System.Text;
using Paramount.Betterclassifieds.Business.Csv;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    /// <summary>
    /// Produces a CSV line for the EventGuestListViewModel class
    /// </summary>
    public class EventGuestListCsvLineProvider : ICsvLineProvider<EventGuestListViewModel>
    {
        private readonly EventTicketField[] _fields;

        public EventGuestListCsvLineProvider(IEnumerable<EventTicket> availableTickets)
        {
            _fields = availableTickets.SelectMany(e => e.EventTicketFields).ToArray();
        }

        public string GetHeader(EventGuestListViewModel guest)
        {
            var builder = new StringBuilder($"Ticket Number,Ticket Name,Guest Email,Guest Full Name,Seat,Promo,Group,Ticket Price,Booking Date,Booking Time");
            
            foreach (var field in _fields)
            {
                builder.AppendFormat(",{0}", field.FieldName);
            }
            return builder.ToString();
        }

        public string GetCsvLine(EventGuestListViewModel guest)
        {
            var builder = new StringBuilder($"{guest.TicketNumber},{guest.TicketName},{guest.GuestEmail},{guest.GuestFullName},{guest.SeatNumber},{guest.PromoCode},{guest.GroupName},{guest.TicketTotalPrice},{guest.DateOfBooking.ToString("dd/MM/yyyy")},{guest.DateOfBooking.ToString("HH:mm")}");
            
            foreach (var field in _fields)
            {
                var guestField = guest.DynamicFields.FirstOrDefault(f => f.FieldName.Equals(field.FieldName, StringComparison.OrdinalIgnoreCase));
                builder.AppendFormat(",{0}", guestField.With(f => f.FieldValue));
            }
            return builder.ToString();
        }
    }
}