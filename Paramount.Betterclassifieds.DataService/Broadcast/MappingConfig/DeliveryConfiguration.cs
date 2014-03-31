using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            ToTable("Notification");

            HasKey(k => k.BroadcastId);

            Property(prop => prop.DocType).HasMaxLength(50);
        }
    }
}