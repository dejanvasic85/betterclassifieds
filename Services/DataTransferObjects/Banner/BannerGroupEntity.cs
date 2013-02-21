using System;

namespace Paramount.Common.DataTransferObjects.Banner
{
    public class BannerGroupEntity
    {
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public bool UseTimer { get; set; }
        public bool IsActive { get; set; }
        public string AcceptedFileType { get; set; }
       
    }
}