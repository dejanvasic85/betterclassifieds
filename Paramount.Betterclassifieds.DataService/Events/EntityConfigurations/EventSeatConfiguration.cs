using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventSeatConfiguration : EntityTypeConfiguration<EventSeat>
    {
        public EventSeatConfiguration()
        {
            ToTable("EventSeat");

            HasKey(prop => prop.EventSeatId);

            HasRequired(prop => prop.EventTicket).WithMany(t => t.EventSeats).HasForeignKey(k => k.EventTicketId);
            
        }
    }
}