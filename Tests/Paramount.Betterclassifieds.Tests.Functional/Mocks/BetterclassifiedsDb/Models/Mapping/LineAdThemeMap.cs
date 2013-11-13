using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class LineAdThemeMap : EntityTypeConfiguration<LineAdTheme>
    {
        public LineAdThemeMap()
        {
            // Primary Key
            this.HasKey(t => t.LineAdThemeId);

            // Properties
            this.Property(t => t.ThemeName)
                .HasMaxLength(100);

            this.Property(t => t.ImageUrl)
                .HasMaxLength(500);

            this.Property(t => t.HeaderColourCode)
                .HasMaxLength(10);

            this.Property(t => t.HeaderColourName)
                .HasMaxLength(50);

            this.Property(t => t.BorderColourCode)
                .HasMaxLength(10);

            this.Property(t => t.BorderColourName)
                .HasMaxLength(50);

            this.Property(t => t.BackgroundColourCode)
                .HasMaxLength(10);

            this.Property(t => t.BackgroundColourName)
                .HasMaxLength(50);

            this.Property(t => t.CreatedByUser)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("LineAdTheme");
            this.Property(t => t.LineAdThemeId).HasColumnName("LineAdThemeId");
            this.Property(t => t.ThemeName).HasColumnName("ThemeName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DescriptionHtml).HasColumnName("DescriptionHtml");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.HeaderColourCode).HasColumnName("HeaderColourCode");
            this.Property(t => t.HeaderColourName).HasColumnName("HeaderColourName");
            this.Property(t => t.BorderColourCode).HasColumnName("BorderColourCode");
            this.Property(t => t.BorderColourName).HasColumnName("BorderColourName");
            this.Property(t => t.BackgroundColourCode).HasColumnName("BackgroundColourCode");
            this.Property(t => t.BackgroundColourName).HasColumnName("BackgroundColourName");
            this.Property(t => t.IsHeadingSuperBold).HasColumnName("IsHeadingSuperBold");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedByUser).HasColumnName("CreatedByUser");
        }
    }
}
