using System.Data;
using System.Web.Services;
using Paramount.Betterclassifieds.DataService;
using Paramount.Common.DataTransferObjects;
using Paramount.Common.DataTransferObjects.LocationService.Messages;
using Paramount.Common.ServiceContracts;

namespace Paramount.Services
{
    [WebService(Namespace = "http://paramountit.com.au/webservices/")]
    public class LocationService : ILocationService
    {
        public GetRegionResponse GetRegion(GetRegionRequest request)
        {
            var response = new GetRegionResponse();
            var region = LocationDataService.GetRegion().Rows;
            foreach (DataRow item in region)
            {
                response.RegionList.Add(new ListItem
                                            {
                                                Code = (string)item["RegionId"].ToString(),
                                                Description = (string)item["Title"]
                                            });
            }
            return response;
        }

        public GetListItemResponse GetCountry(GetListItemRequest request)
        {
            throw new System.NotImplementedException();
        }

        public GetStateListResponse GetStateList(GetStateListRequest request)
        {
            var dataService = new CommonDataProvider(request.ClientCode);
            return new GetStateListResponse() {StateList = dataService.GetStateList(request.Country)};
        }

        public GetCountryListResponse GetCountryList(GetCountryListRequest request)
        {
            var dataService = new CommonDataProvider(request.ClientCode);
            return new GetCountryListResponse() { CountryList = dataService.GetCountryList() };
        }
    }
}
