using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iFlog.Tests.Functional.Mocks.iFlogDb.Mapping
{
    public class AdBookingMap : EntityTypeConfiguration<AdBooking>
    {
        public AdBookingMap()
        {
            // Primary Key
            this.HasKey(t => t.AdBookingId);

            // Properties
            this.Property(t => t.BookReference)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserId)
                .HasMaxLength(50);

            this.Property(t => t.BookingType)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("AdBooking");
            this.Property(t => t.AdBookingId).HasColumnName("AdBookingId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.TotalPrice).HasColumnName("TotalPrice");
            this.Property(t => t.BookReference).HasColumnName("BookReference");
            this.Property(t => t.AdId).HasColumnName("AdId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.BookingStatus).HasColumnName("BookingStatus");
            this.Property(t => t.MainCategoryId).HasColumnName("MainCategoryId");
            this.Property(t => t.BookingType).HasColumnName("BookingType");
            this.Property(t => t.BookingDate).HasColumnName("BookingDate");
            this.Property(t => t.Insertions).HasColumnName("Insertions");

            // Relationships
            this.HasOptional(t => t.Ad)
                .WithMany(t => t.AdBookings)
                .HasForeignKey(d => d.AdId);
            this.HasOptional(t => t.MainCategory)
                .WithMany(t => t.AdBookings)
                .HasForeignKey(d => d.MainCategoryId);

        }
    }
}
