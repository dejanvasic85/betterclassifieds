using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Domain;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds.Mapping
{
    public class TempBookingRecordMap : EntityTypeConfiguration<TempBookingRecord>
    {
        public TempBookingRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.BookingRecordId);

            // Properties
            this.Property(t => t.SessionID)
                .IsRequired()
                .HasMaxLength(25);

            this.Property(t => t.UserId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.AdReferenceId)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("TempBookingRecord");
            this.Property(t => t.BookingRecordId).HasColumnName("BookingRecordId");
            this.Property(t => t.TotalCost).HasColumnName("TotalCost");
            this.Property(t => t.SessionID).HasColumnName("SessionID");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.AdReferenceId).HasColumnName("AdReferenceId");
        }
    }
}
