using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.Business;
using System.Data.Entity;

namespace Paramount.Betterclassifieds.DataService
{
    public class DocumentContext : DbContext
    {
        public DocumentContext()
            : base(ConfigReader.GetConnectionString("paramount/dsl"))
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Document Storage
            modelBuilder.Entity<Document>().ToTable("DocumentStorage");
            modelBuilder.Entity<Document>().HasKey(key => key.DocumentId).Property(prop => prop.DocumentId).HasColumnName("DocumentID");
            modelBuilder.Entity<Document>().Property(prop => prop.Data).HasColumnName("FileData");
            modelBuilder.Entity<Document>().Property(prop => prop.ContentType).HasColumnName("FileType");

        }
    }
}
