using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class EmailDeliveryConfiguration : EntityTypeConfiguration<Email>
    {
        public EmailDeliveryConfiguration()
        {
            ToTable("EmailDelivery");

            HasKey(k => k.EmailDeliveryId);
            Property(prop => prop.BroadcastId).IsRequired();
            Property(prop => prop.DocType).HasMaxLength(50);
            Property(prop => prop.To).HasMaxLength(200);
            Property(prop => prop.Cc).HasMaxLength(200);
            Property(prop => prop.Bcc).HasMaxLength(200);
            Property(prop => prop.From).HasMaxLength(200);
            Property(prop => prop.Subject).HasMaxLength(200);
            Property(prop => prop.LastErrorMessage).HasMaxLength(200);
        }
    }
}