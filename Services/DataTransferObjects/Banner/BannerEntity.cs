namespace Paramount.Common.DataTransferObjects.Banner
{
    using System;
    using System.Collections.ObjectModel;
    public class BannerEntity
    {
        public string Title { get; set;}

        public string Description { get; set; }

        public Guid BannerId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid ImageId { get; set; }

        public Guid GroupId { get; set; }

        public string NavigateUrl { get; set; }

        public int RequestCount { get; set; }

        public bool IsDeleted { get; set; }

        public string CreatedBy { get; set; }

        public int ClickCount { get; set; }

        public string ClientCode { get; set; }

        public string GroupName { get; set; }

        public Collection<CodeDescription> Attributes { get; set; }

        public BannerEntity ()
        {
            Attributes = new Collection<CodeDescription>();
        }
    }
}