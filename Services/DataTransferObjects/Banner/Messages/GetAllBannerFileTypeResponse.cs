using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Paramount.Common.DataTransferObjects.Banner.Messages
{
    public class GetAllBannerFileTypeResponse
    {
        public List<CodeDescription> BannerFileTypes { get; set; }

        public GetAllBannerFileTypeResponse()
        {
            BannerFileTypes = new List<CodeDescription>();
        }
    }
}