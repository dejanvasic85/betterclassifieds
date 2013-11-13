using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class PublicationMap : EntityTypeConfiguration<Publication>
    {
        public PublicationMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicationId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(255);

            this.Property(t => t.FrequencyType)
                .HasMaxLength(20);

            this.Property(t => t.FrequencyValue)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Publication");
            this.Property(t => t.PublicationId).HasColumnName("PublicationId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.PublicationTypeId).HasColumnName("PublicationTypeId");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.FrequencyType).HasColumnName("FrequencyType");
            this.Property(t => t.FrequencyValue).HasColumnName("FrequencyValue");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");

            // Relationships
            this.HasOptional(t => t.PublicationType)
                .WithMany(t => t.Publications)
                .HasForeignKey(d => d.PublicationTypeId);

        }
    }
}
