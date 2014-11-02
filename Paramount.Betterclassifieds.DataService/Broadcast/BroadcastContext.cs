﻿using System.Configuration;
using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.Business.Broadcast;
using System.Data.Entity;

namespace Paramount.Betterclassifieds.DataService.Broadcast
{
    public class BroadcastContext : DbContext
    {
        public BroadcastContext()
            : base(ConfigurationManager.ConnectionStrings["BroadcastConnection"].ConnectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Notification> Notifications { get; set; } 
        public IDbSet<Email> Emails { get; set; }
        public IDbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new EmailDeliveryConfiguration());
            modelBuilder.Configurations.Add(new EmailTemplateConfiguration());
        }
    }
}
