namespace Paramount.Betterclassifieds.DataLayer.Configurations
{
    public class BookEntryConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Business.BookEntry>
    {
        public BookEntryConfiguration()
        {
            ToTable("BookEntry");

            HasKey(k => k.BookEntryId);
        }
    }
}