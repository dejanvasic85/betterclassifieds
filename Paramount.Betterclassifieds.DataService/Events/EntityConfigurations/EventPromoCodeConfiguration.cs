using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventPromoCodeConfiguration : EntityTypeConfiguration<EventPromoCode>
    {
        public EventPromoCodeConfiguration()
        {
            ToTable("EventPromoCode");
            HasKey(prop => prop.EventPromoCodeId);
            Property(prop => prop.PromoCode).IsRequired().HasMaxLength(50);
            Property(prop => prop.DiscountPercent).IsOptional();

            // Relationships
            HasRequired(prop => prop.Event).WithMany(prop => prop.PromoCodes).HasForeignKey(prop => prop.EventId);
        }
    }
}