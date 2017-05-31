using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventSeatBookingConfiguration : EntityTypeConfiguration<EventSeatBooking>
    {
        public EventSeatBookingConfiguration()
        {
            ToTable("EventSeatBooking");
            HasKey(prop => prop.EventSeatBookingId);
            HasRequired(prop => prop.EventTicket).WithMany(t => t.EventSeats).HasForeignKey(k => k.EventTicketId);
        }
    }
}