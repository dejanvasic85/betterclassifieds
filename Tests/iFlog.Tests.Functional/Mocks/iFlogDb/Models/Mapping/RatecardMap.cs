using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class RatecardMap : EntityTypeConfiguration<Ratecard>
    {
        public RatecardMap()
        {
            // Primary Key
            this.HasKey(t => t.RatecardId);

            // Properties
            this.Property(t => t.CreatedByUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Ratecard");
            this.Property(t => t.RatecardId).HasColumnName("RatecardId");
            this.Property(t => t.BaseRateId).HasColumnName("BaseRateId");
            this.Property(t => t.MinCharge).HasColumnName("MinCharge");
            this.Property(t => t.MaxCharge).HasColumnName("MaxCharge");
            this.Property(t => t.RatePerMeasureUnit).HasColumnName("RatePerMeasureUnit");
            this.Property(t => t.MeasureUnitLimit).HasColumnName("MeasureUnitLimit");
            this.Property(t => t.PhotoCharge).HasColumnName("PhotoCharge");
            this.Property(t => t.BoldHeading).HasColumnName("BoldHeading");
            this.Property(t => t.OnlineEditionBundle).HasColumnName("OnlineEditionBundle");
            this.Property(t => t.LineAdSuperBoldHeading).HasColumnName("LineAdSuperBoldHeading");
            this.Property(t => t.LineAdColourHeading).HasColumnName("LineAdColourHeading");
            this.Property(t => t.LineAdColourBorder).HasColumnName("LineAdColourBorder");
            this.Property(t => t.LineAdColourBackground).HasColumnName("LineAdColourBackground");
            this.Property(t => t.LineAdExtraImage).HasColumnName("LineAdExtraImage");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedByUser).HasColumnName("CreatedByUser");

            // Relationships
            this.HasOptional(t => t.BaseRate)
                .WithMany(t => t.Ratecards)
                .HasForeignKey(d => d.BaseRateId);

        }
    }
}
