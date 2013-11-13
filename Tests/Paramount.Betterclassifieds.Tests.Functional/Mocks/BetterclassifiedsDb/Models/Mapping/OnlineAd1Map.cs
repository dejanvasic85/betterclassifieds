using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks.BetterclassifiedsDb.Mapping
{
    public class OnlineAd1Map : EntityTypeConfiguration<OnlineAd1>
    {
        public OnlineAd1Map()
        {
            // Primary Key
            this.HasKey(t => new { t.OnlineAdId, t.AdBookingId, t.AdId });

            // Properties
            this.Property(t => t.OnlineAdId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Heading)
                .HasMaxLength(255);

            this.Property(t => t.ContactName)
                .HasMaxLength(200);

            this.Property(t => t.ContactType)
                .HasMaxLength(20);

            this.Property(t => t.ContactValue)
                .HasMaxLength(100);

            this.Property(t => t.UserId)
                .HasMaxLength(50);

            this.Property(t => t.BookReference)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.BookingType)
                .HasMaxLength(20);

            this.Property(t => t.AdBookingId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Category)
                .HasMaxLength(50);

            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Area)
                .HasMaxLength(50);

            this.Property(t => t.AdId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("OnlineAds");
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
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.TotalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.BookReference).HasColumnName("BookReference");
            this.Property(t => t.BookingStatus).HasColumnName("BookingStatus");
            this.Property(t => t.MainCategoryId).HasColumnName("MainCategoryId");
            this.Property(t => t.Insertions).HasColumnName("Insertions");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.BookingType).HasColumnName("BookingType");
            this.Property(t => t.AdBookingId).HasColumnName("AdBookingId");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.AdId).HasColumnName("AdId");
        }
    }
}
