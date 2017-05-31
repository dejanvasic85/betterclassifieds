using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventSeatingContract
    {
        public string VenueName { get; set; }
        public IEnumerable<EventTicketContract> Tickets { get; set; }
        public IEnumerable<EventRowContract> Rows { get; set; }
    }

    public class EventSeatingContractFactory
    {
        private EventTicketContractFactory _ticketFactory;
        private EventRowContractFactory _rowFactory;

        public EventSeatingContractFactory()
        {
            _ticketFactory = new EventTicketContractFactory();
            _rowFactory = new EventRowContractFactory();
        }

        public EventSeatingContract FromModels(EventModel eventDetails, IEnumerable<EventTicket> tickets, IEnumerable<EventSeatBooking> seats)
        {
            var seatsGroupedByRow = seats.GroupBy(s => s.RowNumber);

            return new EventSeatingContract
            {
                VenueName = eventDetails.VenueName,
                Tickets = tickets.Select(t => _ticketFactory.FromModel(t)),
                Rows = _rowFactory.FromModels(seatsGroupedByRow)
            };
        }
    }
}