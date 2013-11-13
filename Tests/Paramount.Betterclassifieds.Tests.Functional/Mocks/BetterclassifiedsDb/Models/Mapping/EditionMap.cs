using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class EditionMap : EntityTypeConfiguration<Edition>
    {
        public EditionMap()
        {
            // Primary Key
            this.HasKey(t => t.EditionId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Edition");
            this.Property(t => t.EditionId).HasColumnName("EditionId");
            this.Property(t => t.PublicationId).HasColumnName("PublicationId");
            this.Property(t => t.EditionDate).HasColumnName("EditionDate");
            this.Property(t => t.Deadline).HasColumnName("Deadline");
            this.Property(t => t.Active).HasColumnName("Active");

            // Relationships
            this.HasOptional(t => t.Publication)
                .WithMany(t => t.Editions)
                .HasForeignKey(d => d.PublicationId);

        }
    }
}
