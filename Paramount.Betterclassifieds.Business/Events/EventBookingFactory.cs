using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingFactory
    {

        public EventBooking Create(int eventId, 
            ApplicationUser applicationUser, 
            IEnumerable<EventTicketReservation> currentReservations,
            DateTime createdDate,
            DateTime createdDateUtc)
        {
            var reservations = currentReservations.ToList();
            var eventBooking = new EventBooking
            {
                EventId = eventId,
                CreatedDateTimeUtc = createdDateUtc,
                CreatedDateTime = createdDate,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
                Phone = applicationUser.Phone,
                PostCode = applicationUser.Postcode,
                UserId = applicationUser.Username
            };

            // Add the ticket bookings
            var eventBookingTicketFactory = new EventBookingTicketFactory();
            eventBooking.EventBookingTickets.AddRange(reservations.SelectMany(r => eventBookingTicketFactory.CreateFromReservation(r, createdDate, createdDateUtc)));

            // Calculate the total
            eventBooking.TotalCost = reservations.Sum(r => r.Price.GetValueOrDefault() * r.Quantity);

            // Ensure the status is payment pending if there's a total cost larger than zero
            eventBooking.Status = eventBooking.TotalCost > 0 
                ? EventBookingStatus.PaymentPending
                : EventBookingStatus.Active;

            return eventBooking;
        }
    }
}