namespace Paramount.Common.ServiceContracts
{
    using System.Web.Services;
    using DataTransferObjects.CRM.Messages;

    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public interface ICRMService
    {
        [WebMethod(EnableSession = true)]
        GetEntityListResponse GetEntityList(GetEntityListRequest request);

        [WebMethod(EnableSession = true)]
        GetEntityResponse GetParamountEntity(GetEntityRequest request);

        [WebMethod(EnableSession = true)]
        GetModuleListResponse GetModuleList(GetModuleListRequest request);

        [WebMethod(EnableSession = true)]
        UpdateCreateEntityResponse UpdateCreateEntity(UpdateCreateEntityRequest request);

        [WebMethod(EnableSession = true)]
        GetModulesByEntityCodeResponse GetModuleListByEntity(GetModulesByEntityCodeRequest request);

        [WebMethod(EnableSession = true)]
        UpdateCreateEntityModuleResponse UpdateCreateEnityModule(UpdateCreateEntityModuleRequest request);
    }
}
