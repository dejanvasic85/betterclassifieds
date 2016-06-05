using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingTicketFactory
    {
        private readonly IEventRepository _eventRepository;

        public EventBookingTicketFactory(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IEnumerable<EventBookingTicket> CreateFromReservation(EventTicketReservation reservation,
            DateTime createdDate, DateTime createdDateUtc)
        {
            if (!reservation.EventTicketId.HasValue)
                throw new ArgumentNullException("reservation", "EventTicketId cannot be null in the reservation");

            var eventTicket = _eventRepository.GetEventTicketDetails(reservation.EventTicketId.GetValueOrDefault());
            
            for (int i = 0; i < reservation.Quantity; i++)
            {
                yield return new EventBookingTicket
                {
                    EventTicketId = eventTicket.EventTicketId.GetValueOrDefault(),
                    TicketName = eventTicket.TicketName,
                    CreatedDateTime = createdDate,
                    CreatedDateTimeUtc = createdDateUtc,
                    Price = reservation.Price,
                    TransactionFee = reservation.TransactionFee,
                    TotalPrice = reservation.TotalPriceWithTxnFee,
                    GuestEmail = reservation.GuestEmail,
                    GuestFullName = reservation.GuestFullName,
                    TicketFieldValues = reservation.TicketFields.Select(r => new EventBookingTicketField { FieldName = r.FieldName, FieldValue = r.FieldValue }).ToList()
                };
            }
        }
    }
}