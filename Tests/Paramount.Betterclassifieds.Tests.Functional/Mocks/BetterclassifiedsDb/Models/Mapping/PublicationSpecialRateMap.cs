using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class PublicationSpecialRateMap : EntityTypeConfiguration<PublicationSpecialRate>
    {
        public PublicationSpecialRateMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicationSpecialRateId);

            // Properties
            // Table & Column Mappings
            this.ToTable("PublicationSpecialRate");
            this.Property(t => t.PublicationSpecialRateId).HasColumnName("PublicationSpecialRateId");
            this.Property(t => t.SpecialRateId).HasColumnName("SpecialRateId");
            this.Property(t => t.PublicationAdTypeId).HasColumnName("PublicationAdTypeId");
            this.Property(t => t.PublicationCategoryId).HasColumnName("PublicationCategoryId");

            // Relationships
            this.HasOptional(t => t.PublicationAdType)
                .WithMany(t => t.PublicationSpecialRates)
                .HasForeignKey(d => d.PublicationAdTypeId);
            this.HasOptional(t => t.PublicationCategory)
                .WithMany(t => t.PublicationSpecialRates)
                .HasForeignKey(d => d.PublicationCategoryId);
            this.HasOptional(t => t.SpecialRate)
                .WithMany(t => t.PublicationSpecialRates)
                .HasForeignKey(d => d.SpecialRateId);

        }
    }
}
