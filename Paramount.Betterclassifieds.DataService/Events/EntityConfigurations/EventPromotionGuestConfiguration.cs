using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class EventPromotionGuestConfiguration : EntityTypeConfiguration<EventPromotionGuest>
    {
        public EventPromotionGuestConfiguration()
        {
            ToTable("EventPromotionGuest");
            HasKey(p => p.EventPromotionGuestId);

            HasRequired(prop => prop.EventModel);
        }
    }
}