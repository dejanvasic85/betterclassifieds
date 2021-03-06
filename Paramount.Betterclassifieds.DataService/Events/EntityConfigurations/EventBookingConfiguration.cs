using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventBookingConfiguration : EntityTypeConfiguration<EventBooking>
    {
        public EventBookingConfiguration()
        {
            ToTable("EventBooking");
            HasKey(prop => prop.EventBookingId);
            HasMany(prop => prop.EventBookingTickets).WithRequired(prop => prop.EventBooking);
            HasRequired(prop => prop.Event).WithMany(prop => prop.EventBookings).HasForeignKey(prop => prop.EventId);

            Property(prop => prop.StatusAsString).HasColumnName("Status");
            Property(prop => prop.PaymentMethodAsString).HasColumnName("PaymentMethod");
            Property(prop => prop.TotalCost).HasPrecision(19, 4);
            Property(prop => prop.Cost).HasPrecision(19, 4);
            Property(prop => prop.TransactionFee).HasPrecision(19, 4);
            Property(prop => prop.FeeCents).HasPrecision(19, 4);
            Property(prop => prop.FeePercentage).HasPrecision(19, 4);

            Ignore(prop => prop.Status);
            Ignore(prop => prop.PaymentMethod);
        }
    }
}