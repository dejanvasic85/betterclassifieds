namespace Paramount.Common.ServiceContracts
{
    using System.ServiceModel;
    using DataTransferObjects.Betterclassifieds.Messages;

    [ServiceContract]
    public interface IBetterclassifiedService
    {
        [OperationContract]
        GetExpiredAdListByLastEditionResponse GetExpiredAdListByLastEdition(GetExpiredAdListByLastEditionRequest request);

    }
}