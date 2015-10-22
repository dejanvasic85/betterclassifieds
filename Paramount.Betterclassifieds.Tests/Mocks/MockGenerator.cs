
using Paramount.Betterclassifieds.Business.Events;
using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Tests
{
	///
	/// Auto-Generated code. See MockGenerator.tt 
	///

	internal partial class EventTicketMockBuilder : MockBuilder<EventTicketMockBuilder, EventTicket>
	{	
		public EventTicketMockBuilder WithEventTicketId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventTicketId = val);
		}
		public EventTicketMockBuilder WithEventId(Int32? val)
		{ 
			return WithBuildStep(p => p.EventId = val);
		}
		public EventTicketMockBuilder WithTicketName(String val)
		{ 
			return WithBuildStep(p => p.TicketName = val);
		}
		public EventTicketMockBuilder WithAvailableQuantity(Int32 val)
		{ 
			return WithBuildStep(p => p.AvailableQuantity = val);
		}
		public EventTicketMockBuilder WithRemainingQuantity(Int32 val)
		{ 
			return WithBuildStep(p => p.RemainingQuantity = val);
		}
		public EventTicketMockBuilder WithPrice(Decimal val)
		{ 
			return WithBuildStep(p => p.Price = val);
		}
		public EventTicketMockBuilder WithEventTicketReservations(IList<EventTicketReservation> val)
		{ 
			return WithBuildStep(p => p.EventTicketReservations = val);
		}
		public EventTicketMockBuilder WithEventBookingTickets(IList<EventBookingTicket> val)
		{ 
			return WithBuildStep(p => p.EventBookingTickets = val);
		}
	}
}


