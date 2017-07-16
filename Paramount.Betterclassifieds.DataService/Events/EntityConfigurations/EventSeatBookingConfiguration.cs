using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventSeatBookingConfiguration : EntityTypeConfiguration<EventSeat>
    {
        public EventSeatBookingConfiguration()
        {
            ToTable("EventSeatBooking");
            HasKey(prop => prop.EventSeatId);
            HasRequired(prop => prop.EventTicket).WithMany(t => t.EventSeats).HasForeignKey(k => k.EventTicketId);
            Ignore(prop => prop.ReservationExpiryUtc);
        }
    }
}