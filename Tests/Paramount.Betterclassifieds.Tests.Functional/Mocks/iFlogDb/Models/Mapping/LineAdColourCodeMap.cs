using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class LineAdColourCodeMap : EntityTypeConfiguration<LineAdColourCode>
    {
        public LineAdColourCodeMap()
        {
            // Primary Key
            this.HasKey(t => t.LineAdColourId);

            // Properties
            this.Property(t => t.LineAdColourName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ColourCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CreatedByUser)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("LineAdColourCode");
            this.Property(t => t.LineAdColourId).HasColumnName("LineAdColourId");
            this.Property(t => t.LineAdColourName).HasColumnName("LineAdColourName");
            this.Property(t => t.ColourCode).HasColumnName("ColourCode");
            this.Property(t => t.Cyan).HasColumnName("Cyan");
            this.Property(t => t.Magenta).HasColumnName("Magenta");
            this.Property(t => t.Yellow).HasColumnName("Yellow");
            this.Property(t => t.KeyCode).HasColumnName("KeyCode");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.CreatedByUser).HasColumnName("CreatedByUser");
        }
    }
}
