using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class SpecialRateMap : EntityTypeConfiguration<SpecialRate>
    {
        public SpecialRateMap()
        {
            // Primary Key
            this.HasKey(t => t.SpecialRateId);

            // Properties
            // Table & Column Mappings
            this.ToTable("SpecialRate");
            this.Property(t => t.SpecialRateId).HasColumnName("SpecialRateId");
            this.Property(t => t.BaseRateId).HasColumnName("BaseRateId");
            this.Property(t => t.NumOfInsertions).HasColumnName("NumOfInsertions");
            this.Property(t => t.MaximumWords).HasColumnName("MaximumWords");
            this.Property(t => t.SetPrice).HasColumnName("SetPrice");
            this.Property(t => t.Discount).HasColumnName("Discount");
            this.Property(t => t.NumOfAds).HasColumnName("NumOfAds");
            this.Property(t => t.LineAdBoldHeader).HasColumnName("LineAdBoldHeader");
            this.Property(t => t.LineAdImage).HasColumnName("LineAdImage");
            this.Property(t => t.NumberOfImages).HasColumnName("NumberOfImages");

            // Relationships
            this.HasOptional(t => t.BaseRate)
                .WithMany(t => t.SpecialRates)
                .HasForeignKey(d => d.BaseRateId);

        }
    }
}
