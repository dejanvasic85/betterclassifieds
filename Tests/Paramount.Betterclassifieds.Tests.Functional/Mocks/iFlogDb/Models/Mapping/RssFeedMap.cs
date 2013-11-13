using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class RssFeedMap : EntityTypeConfiguration<RssFeed>
    {
        public RssFeedMap()
        {
            // Primary Key
            this.HasKey(t => new { t.OnlineAdId, t.AdId });

            // Properties
            this.Property(t => t.OnlineAdId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BookReference)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserId)
                .HasMaxLength(50);

            this.Property(t => t.AdId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Heading)
                .HasMaxLength(255);

            this.Property(t => t.ContactName)
                .HasMaxLength(200);

            this.Property(t => t.CategoryTitle)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("RssFeed");
            this.Property(t => t.OnlineAdId).HasColumnName("OnlineAdId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.BookReference).HasColumnName("BookReference");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.MainCategoryId).HasColumnName("MainCategoryId");
            this.Property(t => t.AdId).HasColumnName("AdId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Heading).HasColumnName("Heading");
            this.Property(t => t.HtmlText).HasColumnName("HtmlText");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.CategoryTitle).HasColumnName("CategoryTitle");
        }
    }
}
