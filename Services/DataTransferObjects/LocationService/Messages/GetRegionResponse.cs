namespace Paramount.Common.DataTransferObjects.LocationService.Messages
{
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using LocationService;

    public class GetRegionResponse
    {
        public Collection<ListItem> RegionList { get; set; }

        public GetRegionResponse()
        {
            RegionList = new Collection<ListItem>();
        }
    }
}
