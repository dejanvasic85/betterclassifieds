using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventBookingTicketConfiguration : EntityTypeConfiguration<EventBookingTicket>
    {
        public EventBookingTicketConfiguration()
        {
            ToTable("EventBookingTicket");
            HasKey(prop => prop.EventBookingTicketId);
            HasRequired(prop => prop.EventTicket).WithMany(prop => prop.EventBookingTickets).HasForeignKey(prop => prop.EventTicketId);
            HasRequired(prop => prop.EventBooking).WithMany(prop => prop.EventBookingTickets).HasForeignKey(prop => prop.EventBookingId);
            Property(prop => prop.Price).HasPrecision(19, 4);
            Property(prop => prop.TransactionFee).HasPrecision(19, 4);
            Property(prop => prop.TotalPrice).HasPrecision(19, 4);
        }
    }
}