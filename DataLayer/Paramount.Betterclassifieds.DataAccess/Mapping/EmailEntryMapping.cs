using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.DataAccess.Mapping
{
    public class EmailEntryMapping : EntityTypeConfiguration<Domain.Notifications.Email>
    {
        public EmailEntryMapping()
        {
            this.HasKey(k => k.Id);

            // Properties
            this.ToTable("EmailBroadcastEntry");

            this.Property(p => p.EmailRecipient).HasColumnName("Email").HasMaxLength(100);
            this.Property(p => p.Id).HasColumnName("EmailBroadcastEntryId");
            this.Property(p => p.Subject).HasColumnName("Subject");
            this.Property(p => p.EmailContent).HasColumnName("EmailContent");
            this.Property(p => p.LastRetryDate).HasColumnName("LastRetryDateTime");
            this.Property(p => p.SentDateTime).HasColumnName("SentDateTime");
            this.Property(p => p.Attempts).HasColumnName("RetryNo");
            this.Property(p => p.Sender).HasColumnName("Sender").HasMaxLength(50);
            this.Property(p => p.IsBodyHtml).HasColumnName("IsBodyHtml");
            this.Property(p => p.CreatedDateTime).HasColumnName("CreatedDateTime");
        }
    }
}