using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public class EventBookingFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IDateService _dateService;
        private readonly TicketFeeCalculator _ticketFeeCalculator;
        private readonly IClientConfig _clientConfig;

        public EventBookingFactory(IEventRepository eventRepository, IDateService dateService, TicketFeeCalculator ticketFeeCalculator, IClientConfig clientConfig)
        {
            _eventRepository = eventRepository;
            _dateService = dateService;
            _ticketFeeCalculator = ticketFeeCalculator;
            _clientConfig = clientConfig;
        }

        public EventBooking Create(EventModel eventModel,
            EventPromoCode eventPromo,
            ApplicationUser applicationUser,
            IEnumerable<EventTicketReservation> currentReservations,
            string howYourHeardAboutEvent)
        {
            var reservations = currentReservations.ToList();
            var bookingCost = _ticketFeeCalculator.GetTotalTicketPrice(reservations, eventPromo, eventModel.IncludeTransactionFee.GetValueOrDefault());

            var eventBooking = new EventBooking
            {
                EventId = eventModel.EventId.GetValueOrDefault(),
                CreatedDateTimeUtc = _dateService.Now,
                CreatedDateTime = _dateService.UtcNow,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Email = applicationUser.Email,
                HowYouHeardAboutEvent = howYourHeardAboutEvent,
                Phone = applicationUser.Phone,
                PostCode = applicationUser.Postcode,
                UserId = applicationUser.Username,
                PromoCode = eventPromo?.PromoCode,
                DiscountPercent = eventPromo?.DiscountPercent,
                DiscountAmount = bookingCost.DiscountAmount,
                Cost = bookingCost.OriginalPrice,
                TransactionFee = bookingCost.Fee,
                TotalCost = bookingCost.Total,
                Status = bookingCost.Total > 0
                    ? EventBookingStatus.PaymentPending
                    : EventBookingStatus.Active,
                FeePercentage = _clientConfig.EventTicketFeePercentage / 100,
                FeeCents = _clientConfig.EventTicketFeeCents

            };

            // Add the ticket bookings
            var eventBookingTicketFactory = new EventBookingTicketFactory(_eventRepository, _dateService, _ticketFeeCalculator);
            eventBooking.EventBookingTickets.AddRange(
                reservations.SelectMany(r => eventBookingTicketFactory.CreateFromReservation(eventModel, r, eventPromo, _dateService.Now, _dateService.UtcNow)));

            return eventBooking;
        }
    }
}