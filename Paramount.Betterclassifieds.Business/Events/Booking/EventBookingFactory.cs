using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;

        public EventBookingFactory(IEventRepository eventRepository, IDateService dateService)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
        }

        public EventBooking Create(int eventId,
            ApplicationUser applicationUser,
            IEnumerable<EventTicketReservation> currentReservations)
        {
            var reservations = currentReservations.ToList();
            var eventBooking = new EventBooking
            {
                EventId = eventId,
                CreatedDateTimeUtc = _dateService.Now,
                CreatedDateTime = _dateService.UtcNow,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
                Phone = applicationUser.Phone,
                PostCode = applicationUser.Postcode,
                UserId = applicationUser.Username,               
            };

            // Add the ticket bookings
            var eventBookingTicketFactory = new EventBookingTicketFactory(_eventRepository, _dateService);
            eventBooking.EventBookingTickets.AddRange(
                reservations.SelectMany(r => eventBookingTicketFactory.CreateFromReservation(r, _dateService.Now, _dateService.UtcNow)));

            // Calculate the total
            eventBooking.Cost = reservations.Sum(r => r.Price.GetValueOrDefault());
            eventBooking.TransactionFee = reservations.Sum(r => r.TransactionFee.GetValueOrDefault());
            eventBooking.TotalCost = eventBooking.Cost + eventBooking.TransactionFee;

            // Ensure the status is payment pending if there's a total cost larger than zero
            eventBooking.Status = eventBooking.TotalCost > 0
                ? EventBookingStatus.PaymentPending
                : EventBookingStatus.Active;

            return eventBooking;
        }
    }
}