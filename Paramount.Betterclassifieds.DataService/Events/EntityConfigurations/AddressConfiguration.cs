using System.Data.Entity.ModelConfiguration;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.DataService.Events
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            ToTable("Address");
            HasKey(prop => prop.AddressId);
        }
    }
}   