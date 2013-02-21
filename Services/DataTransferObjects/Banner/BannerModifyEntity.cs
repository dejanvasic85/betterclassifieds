using System;
using System.Collections.ObjectModel;

namespace Paramount.Common.DataTransferObjects.Banner
{
    public class BannerModifyEntity
    {
        public string Title { get; set; }
        public Guid ImageId { get; set; }

        public Guid GroupId { get; set; }
        public string NavigateUrl { get; set; }
        
        public Collection<CodeDescription> Attributes { get; set; }

        public Guid BannerId { get; set; }

        public BannerModifyEntity()
        {
            Attributes = new Collection<CodeDescription>();
        }
    }
}