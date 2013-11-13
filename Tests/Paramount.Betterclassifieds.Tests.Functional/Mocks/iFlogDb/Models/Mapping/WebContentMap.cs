using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class WebContentMap : EntityTypeConfiguration<WebContent>
    {
        public WebContentMap()
        {
            // Primary Key
            this.HasKey(t => t.WebContentId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.PageId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LastModifiedUser)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("WebContent");
            this.Property(t => t.WebContentId).HasColumnName("WebContentId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.PageId).HasColumnName("PageId");
            this.Property(t => t.WebContent1).HasColumnName("WebContent");
            this.Property(t => t.LastModifiedDate).HasColumnName("LastModifiedDate");
            this.Property(t => t.LastModifiedUser).HasColumnName("LastModifiedUser");
        }
    }
}
