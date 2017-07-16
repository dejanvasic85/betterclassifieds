using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventSeatingService
    {
        IEnumerable<EventSeat> GetSeatsForEvent(int eventId);
        IEnumerable<EventSeat> GetSeatsForTicket(EventTicket eventTicket);
        EventSeat GetEventSeat(int eventId, string seatNumber);
    }

    public class EventSeatingService : IEventSeatingService
    {
        private readonly IEventRepository _repository;
        private readonly ILogService _logService;

        public EventSeatingService(IEventRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
        }

        public IEnumerable<EventSeat> GetSeatsForEvent(int eventId)
        {
            return _repository.GetEventSeats(eventId);
        }

        public IEnumerable<EventSeat> GetSeatsForTicket(EventTicket eventTicket)
        {
            return _repository.GetEventSeatsForTicket(eventTicket.EventTicketId.GetValueOrDefault());
        }
        
        public EventSeat GetEventSeat(int eventTicketId, string seatNumber)
        {
            var eventSeat = _repository.GetEventSeat(eventTicketId, seatNumber);

            if (eventSeat == null)
            {
                throw new NullReferenceException($"Event seat cannot be found for ticket id [{eventTicketId}] seat Number [{seatNumber}]");
            }

            return eventSeat;
        }

        //private IEnumerable<EventSeat> SeatFetchMediator(int eventId, string orderRequestId, Func<IEnumerable<EventSeat>> fetcher)
        //{
        //    // Retrieves the current reservations for the event that is NOT for the current session
        //    // Just so that the reservation expiry can be set on the seats coming back from the eventSeatBooking table.

        //    // TODO : Kill this code along with the ReservationExpiryUtc field and EventBookingTicketId field on the eventSeat 
        //    // And this problem should just go away. 
        //    var reservations = _repository.GetCurrentReservationsForEvent(eventId)
        //        .Where(r => r.Status == EventTicketReservationStatus.Reserved)
        //        .Where(r => r.SessionId != orderRequestId);

        //    var seats = fetcher().ToDictionary(s => s.SeatNumber);

        //    foreach (var reservation in reservations)
        //    {
        //        try
        //        {
        //            seats[reservation.SeatNumber].ReservationExpiryUtc = reservation.ExpiryDateUtc;
        //        }
        //        catch (Exception ex)
        //        {
        //            var seatsStr = string.Join(",", seats.Select(s => s.Key).ToArray());
        //            _logService.Error($"The seat number [{reservation.SeatNumber}] is not within the reservation seats: [{seatsStr}]. Please investigate!", ex);
        //        }
        //    }

        //    return seats.Values;
        //}
    }
}