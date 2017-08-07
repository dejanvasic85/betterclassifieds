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
            : this(new EventTicketContractFactory(), new EventRowContractFactory())
        {
        }

        public EventSeatingContractFactory(EventTicketContractFactory ticketContractFactory,
            EventRowContractFactory rowContractFactory)
        {
            _ticketFactory = ticketContractFactory;
            _rowFactory = rowContractFactory;
        }

        public EventSeatingContract FromModels(EventModel eventDetails, IEnumerable<EventTicket> tickets,
            IEnumerable<EventSeat> seats)
        {
            var seatsGroupedByRow = seats.GroupBy(s => s.RowNumber);
            var eventTicketContracts = tickets.Select(t => _ticketFactory.FromModel(t)).ToList();
            var rowContracts = _rowFactory.FromModels(seatsGroupedByRow).ToList();
            
            return new EventSeatingContract
            {
                VenueName = eventDetails.VenueName,
                Tickets = eventTicketContracts,
                Rows = rowContracts
            };
        }
    }
}