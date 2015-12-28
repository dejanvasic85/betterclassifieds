using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business.Broadcast;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class EmailAttachmentConfiguration : EntityTypeConfiguration<EmailAttachment>
    {
        public EmailAttachmentConfiguration()
        {
            ToTable("EmailAttachment");
            HasKey(k => k.EmailAttachmentId);
            Property(prop => prop.Content).HasColumnName("AttachmentContent");

            HasRequired(prop => prop.Email)
                .WithMany(prop => prop.EmailAttachments)
                .HasForeignKey(k => k.EmailDeliveryId);
        }
    }
}