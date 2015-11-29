using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketFactory
    {
        private readonly IDateService _dateService;

        public EventBookingTicketFactory(IDateService dateService)
        {
            this._dateService = dateService;
        }

        public IEnumerable<EventBookingTicket> CreateFromReservation(EventTicketReservation reservation)
        {
            if (!reservation.EventTicketId.HasValue)
                throw new ArgumentNullException("reservation", "EventTicketId cannot be null in the reservation");

            if (reservation.EventTicket == null)
                throw new ArgumentNullException("reservation", "EventTicket cannot be null in the reservation");

            for (int i = 0; i < reservation.Quantity; i++)
            {
                yield return new EventBookingTicket
                {
                    EventTicketId = reservation.EventTicketId.Value,
                    TicketName = reservation.EventTicket.TicketName,
                    CreatedDateTime = _dateService.Now,
                    CreatedDateTimeUtc = _dateService.UtcNow,
                    Price = reservation.Price,
                    GuestEmail = reservation.GuestEmail,
                    GuestFullName = reservation.GuestFullName,
                    TicketFieldValues = reservation.TicketFields.Select(r => new EventBookingTicketField { FieldName = r.FieldName, FieldValue = r.FieldValue }).ToList()
                };
            }
        }
    }
}