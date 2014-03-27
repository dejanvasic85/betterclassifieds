using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using System;
using System.Data.Entity;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class BroadcastContext : DbContext
    {
        public BroadcastContext()
            : base(ConfigReader.GetConnectionString("paramount/broadcast"))
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<EmailDelivery> Emails { get; set; }
        public IDbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailDelivery>().ToTable("EmailDelivery");
            modelBuilder.Entity<EmailDelivery>().HasKey(key => key.BroadcastId);

            modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplate");
            modelBuilder.Entity<EmailTemplate>().HasKey(key => key.EmailTemplateId);

        }
    }
}
