using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventPaymentRequestConfiguration : EntityTypeConfiguration<EventPaymentRequest>
    {
        public EventPaymentRequestConfiguration()
        {
            ToTable("EventPaymentRequest");
            HasKey(prop => prop.EventPaymentRequestId);
            Property(prop => prop.PaymentMethodAsString).HasColumnName("PaymentMethod");
            Property(prop => prop.RequestedAmount).HasPrecision(19, 4);
            Ignore(prop => prop.PaymentMethod);
        }
    }
}