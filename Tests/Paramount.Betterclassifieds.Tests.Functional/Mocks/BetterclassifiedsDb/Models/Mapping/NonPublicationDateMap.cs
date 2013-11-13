using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class NonPublicationDateMap : EntityTypeConfiguration<NonPublicationDate>
    {
        public NonPublicationDateMap()
        {
            // Primary Key
            this.HasKey(t => t.NonPublicationDateId);

            // Properties
            // Table & Column Mappings
            this.ToTable("NonPublicationDate");
            this.Property(t => t.NonPublicationDateId).HasColumnName("NonPublicationDateId");
            this.Property(t => t.PublicationId).HasColumnName("PublicationId");
            this.Property(t => t.EditionDate).HasColumnName("EditionDate");

            // Relationships
            this.HasRequired(t => t.Publication)
                .WithMany(t => t.NonPublicationDates)
                .HasForeignKey(d => d.PublicationId);

        }
    }
}
