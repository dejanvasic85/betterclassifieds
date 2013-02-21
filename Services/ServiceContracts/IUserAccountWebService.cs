namespace Paramount.Common.ServiceContracts
{
    using System.ServiceModel;
    using DataTransferObjects.UserAccountWebService.Messages;

    [ServiceContract]
    public interface IUserAccountWebService
    {
        [OperationContract]
        GetNewsletterUsersResponse GetNewsletterUsers(GetNewsletterUsersRequest request);

        [OperationContract]
        void UnsubscribUser(UnSubscribeUserRequest request);
    }
}