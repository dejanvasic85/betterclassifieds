namespace Paramount.Betterclassifieds.DataLayer.Configurations
{
    public class AdBookingConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Business.AdBooking>
    {
        public AdBookingConfiguration()
        {
            ToTable("AdBooking");

            HasKey(k => k.AdBookingId);

            HasMany(fk => fk.BookEntries)
                .WithRequired(k => k.AdBooking)
                .HasForeignKey(fk => fk.AdBookingId)
                .WillCascadeOnDelete(true);
        }
    }
}