using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class LineAdMap : EntityTypeConfiguration<LineAd>
    {
        public LineAdMap()
        {
            // Primary Key
            this.HasKey(t => t.LineAdId);

            // Properties
            this.Property(t => t.AdHeader)
                .HasMaxLength(255);

            this.Property(t => t.AdText)
                .IsRequired();

            this.Property(t => t.BoldHeadingColourCode)
                .HasMaxLength(10);

            this.Property(t => t.BorderColourCode)
                .HasMaxLength(10);

            this.Property(t => t.BackgroundColourCode)
                .HasMaxLength(10);

            this.Property(t => t.FooterPhotoId)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("LineAd");
            this.Property(t => t.LineAdId).HasColumnName("LineAdId");
            this.Property(t => t.AdDesignId).HasColumnName("AdDesignId");
            this.Property(t => t.AdHeader).HasColumnName("AdHeader");
            this.Property(t => t.AdText).HasColumnName("AdText");
            this.Property(t => t.NumOfWords).HasColumnName("NumOfWords");
            this.Property(t => t.UsePhoto).HasColumnName("UsePhoto");
            this.Property(t => t.UseBoldHeader).HasColumnName("UseBoldHeader");
            this.Property(t => t.IsColourBoldHeading).HasColumnName("IsColourBoldHeading");
            this.Property(t => t.IsColourBorder).HasColumnName("IsColourBorder");
            this.Property(t => t.IsColourBackground).HasColumnName("IsColourBackground");
            this.Property(t => t.IsSuperBoldHeading).HasColumnName("IsSuperBoldHeading");
            this.Property(t => t.IsFooterPhoto).HasColumnName("IsFooterPhoto");
            this.Property(t => t.BoldHeadingColourCode).HasColumnName("BoldHeadingColourCode");
            this.Property(t => t.BorderColourCode).HasColumnName("BorderColourCode");
            this.Property(t => t.BackgroundColourCode).HasColumnName("BackgroundColourCode");
            this.Property(t => t.FooterPhotoId).HasColumnName("FooterPhotoId");
            this.Property(t => t.IsSuperHeadingPurchased).HasColumnName("IsSuperHeadingPurchased");

            // Relationships
            this.HasRequired(t => t.AdDesign)
                .WithMany(t => t.LineAds)
                .HasForeignKey(d => d.AdDesignId);

        }
    }
}
