using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class PublicationRateMap : EntityTypeConfiguration<PublicationRate>
    {
        public PublicationRateMap()
        {
            // Primary Key
            this.HasKey(t => t.PublicationRateId);

            // Properties
            // Table & Column Mappings
            this.ToTable("PublicationRate");
            this.Property(t => t.PublicationRateId).HasColumnName("PublicationRateId");
            this.Property(t => t.PublicationAdTypeId).HasColumnName("PublicationAdTypeId");
            this.Property(t => t.PublicationCategoryId).HasColumnName("PublicationCategoryId");
            this.Property(t => t.RatecardId).HasColumnName("RatecardId");

            // Relationships
            this.HasOptional(t => t.PublicationAdType)
                .WithMany(t => t.PublicationRates)
                .HasForeignKey(d => d.PublicationAdTypeId);
            this.HasOptional(t => t.PublicationCategory)
                .WithMany(t => t.PublicationRates)
                .HasForeignKey(d => d.PublicationCategoryId);
            this.HasOptional(t => t.Ratecard)
                .WithMany(t => t.PublicationRates)
                .HasForeignKey(d => d.RatecardId);

        }
    }
}
