using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class AdBookingExtensionMap : EntityTypeConfiguration<AdBookingExtension>
    {
        public AdBookingExtensionMap()
        {
            // Primary Key
            this.HasKey(t => t.AdBookingExtensionId);

            // Properties
            this.Property(t => t.LastModifiedUserId)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("AdBookingExtension");
            this.Property(t => t.AdBookingExtensionId).HasColumnName("AdBookingExtensionId");
            this.Property(t => t.AdBookingId).HasColumnName("AdBookingId");
            this.Property(t => t.Insertions).HasColumnName("Insertions");
            this.Property(t => t.ExtensionPrice).HasColumnName("ExtensionPrice");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.LastModifiedUserId).HasColumnName("LastModifiedUserId");
            this.Property(t => t.LastModifiedDate).HasColumnName("LastModifiedDate");
        }
    }
}
