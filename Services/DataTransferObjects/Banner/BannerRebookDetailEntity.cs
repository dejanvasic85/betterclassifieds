using System.Collections.ObjectModel;

namespace Paramount.Common.DataTransferObjects.Banner
{
    using System;
    public class BannerRebookDetailEntity : BannerModifyEntity
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public BannerRebookDetailEntity()
        {
            Attributes = new Collection<CodeDescription>();
        }
    }
}