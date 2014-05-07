using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.DataService
{
    public class ClassifiedsEntityContext : DbContext
    {
        public ClassifiedsEntityContext()
            : base(ConfigReader.GetConnectionString("paramount/services", "BetterclassifiedsConnection"))
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
