using System;
using System.ServiceModel;
using Paramount.Common.DataTransferObjects;

namespace Paramount.Common.ServiceContracts
{
    using System.Web.Services;
    using DataTransferObjects.LocationService.Messages;

    [WebServiceBinding(Namespace = "http://paramountit.com.au/webservices/")]
    public interface ILocationService
    {
        [WebMethod(EnableSession = true)]
        GetRegionResponse GetRegion(GetRegionRequest request);

        [WebMethod(EnableSession = true)]
        GetListItemResponse GetCountry(GetListItemRequest request);
        
        [OperationContract]
        GetStateListResponse GetStateList(GetStateListRequest request);
        
        [OperationContract]
        GetCountryListResponse GetCountryList(GetCountryListRequest request);
    }
}
