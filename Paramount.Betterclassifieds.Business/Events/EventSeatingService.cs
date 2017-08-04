using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Paramount.Betterclassifieds.Business.Events
{
    public interface IEventSeatingService
    {
        IEnumerable<EventSeat> GetSeatsForEvent(int eventId, string orderRequestId = "");
        IEnumerable<EventSeat> GetSeatsForTicket(EventTicket eventTicket);
        EventSeat GetSeat(int eventId, string seatNumber, string orderRequestId = "");
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

        public IEnumerable<EventSeat> GetSeatsForEvent(int eventId, string orderRequestId = "")
        {
            return Search(eventId, eventTicketId: null, seatNumber: null, orderRequestId: orderRequestId);
        }

        public IEnumerable<EventSeat> GetSeatsForTicket(EventTicket eventTicket)
        {
            return Search(eventTicket.EventId.GetValueOrDefault(), eventTicketId: eventTicket.EventTicketId);
        }

        public EventSeat GetSeat(int eventId, string seatNumber, string orderRequestId = "")
        {
            var results = Search(eventId,
                eventTicketId: null,
                seatNumber: seatNumber,
                orderRequestId: orderRequestId);

            if (results == null)
            {
                throw new NullReferenceException($"Event seat cannot be found for event id [{eventId}] seat Number [{seatNumber}]");
            }

            return results.SingleOrDefault();
        }

        private IEnumerable<EventSeat> Search(int eventId, int? eventTicketId = null, string seatNumber = "", string orderRequestId = "")
        {
            _logService.Info("Searching seat started...");
            var duration = new Stopwatch();
            duration.Start();

            var seats = _repository.GetEventSeats(eventId, eventTicketId, seatNumber, orderRequestId).ToList();

            _logService.Info("Searching seat completed", duration.Elapsed);
            return seats;
        }
    }
}