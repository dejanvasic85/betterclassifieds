using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class EventRowContract
    {
        public string RowName { get; set; }
        public EventSeatBookingContract[] Seats { get; set; }
    }

    public class EventRowContractFactory
    {
        private readonly EventSeatBookingContractFactory _seatFactory;

        public EventRowContractFactory(EventSeatBookingContractFactory seatFactory)
        {
            _seatFactory = seatFactory;
        }

        public EventRowContractFactory()
            : this(new EventSeatBookingContractFactory())
        {
        }

        public IEnumerable<EventRowContract> FromModels(IEnumerable<IGrouping<string, EventSeatBooking>> seatsGroupedByRow)
        {
            foreach (var grouping in seatsGroupedByRow)
            {
                yield return new EventRowContract
                {
                    RowName = grouping.Key,
                    Seats = grouping.Select(s => _seatFactory.FromModel(s)).ToArray()
                };
            }
        }
    }
}