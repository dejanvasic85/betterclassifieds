using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class BookEntryMap : EntityTypeConfiguration<BookEntry>
    {
        public BookEntryMap()
        {
            // Primary Key
            this.HasKey(t => t.BookEntryId);

            // Properties
            this.Property(t => t.RateType)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("BookEntry");
            this.Property(t => t.BookEntryId).HasColumnName("BookEntryId");
            this.Property(t => t.EditionDate).HasColumnName("EditionDate");
            this.Property(t => t.AdBookingId).HasColumnName("AdBookingId");
            this.Property(t => t.PublicationId).HasColumnName("PublicationId");
            this.Property(t => t.EditionAdPrice).HasColumnName("EditionAdPrice");
            this.Property(t => t.PublicationPrice).HasColumnName("PublicationPrice");
            this.Property(t => t.BaseRateId).HasColumnName("BaseRateId");
            this.Property(t => t.RateType).HasColumnName("RateType");

            // Relationships
            this.HasOptional(t => t.AdBooking)
                .WithMany(t => t.BookEntries)
                .HasForeignKey(d => d.AdBookingId);
            this.HasOptional(t => t.Publication)
                .WithMany(t => t.BookEntries)
                .HasForeignKey(d => d.PublicationId);

        }
    }
}
