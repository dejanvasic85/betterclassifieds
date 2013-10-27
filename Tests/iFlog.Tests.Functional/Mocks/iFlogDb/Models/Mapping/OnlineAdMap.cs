using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class OnlineAdMap : EntityTypeConfiguration<OnlineAd>
    {
        public OnlineAdMap()
        {
            // Primary Key
            this.HasKey(t => t.OnlineAdId);

            // Properties
            this.Property(t => t.Heading)
                .HasMaxLength(255);

            this.Property(t => t.ContactName)
                .HasMaxLength(200);

            this.Property(t => t.ContactType)
                .HasMaxLength(20);

            this.Property(t => t.ContactValue)
                .HasMaxLength(100);

            this.Property(t => t.OnlineAdTag)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("OnlineAd");
            this.Property(t => t.OnlineAdId).HasColumnName("OnlineAdId");
            this.Property(t => t.AdDesignId).HasColumnName("AdDesignId");
            this.Property(t => t.Heading).HasColumnName("Heading");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.HtmlText).HasColumnName("HtmlText");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.LocationId).HasColumnName("LocationId");
            this.Property(t => t.LocationAreaId).HasColumnName("LocationAreaId");
            this.Property(t => t.ContactName).HasColumnName("ContactName");
            this.Property(t => t.ContactType).HasColumnName("ContactType");
            this.Property(t => t.ContactValue).HasColumnName("ContactValue");
            this.Property(t => t.NumOfViews).HasColumnName("NumOfViews");
            this.Property(t => t.OnlineAdTag).HasColumnName("OnlineAdTag");

            // Relationships
            this.HasOptional(t => t.AdDesign)
                .WithMany(t => t.OnlineAds)
                .HasForeignKey(d => d.AdDesignId);
            this.HasOptional(t => t.Location)
                .WithMany(t => t.OnlineAds)
                .HasForeignKey(d => d.LocationId);
            this.HasOptional(t => t.LocationArea)
                .WithMany(t => t.OnlineAds)
                .HasForeignKey(d => d.LocationAreaId);

        }
    }
}
