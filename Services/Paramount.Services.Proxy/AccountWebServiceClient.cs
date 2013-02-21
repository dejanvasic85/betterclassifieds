
namespace Paramount.Services.Proxy
{
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using Paramount.Common.DataTransferObjects.DSL.Messages;
    using Paramount.Common.ServiceContracts;
    using Paramount.Common.DataTransferObjects.UserAccountWebService.Messages;

    public partial class AccountWebServiceClient : ClientBase<IUserAccountWebService>
    {
        public AccountWebServiceClient() { }

        public AccountWebServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public AccountWebServiceClient(Binding binding, EndpointAddress endpointAddress)
            : base(binding, endpointAddress) { }

        public GetNewsletterUsersResponse GetNewsletterUsers(GetNewsletterUsersRequest request)
        {
            return Channel.GetNewsletterUsers(request);
        }

        public void UnsubscribUser(UnSubscribeUserRequest request)
        {
            Channel.UnsubscribUser(request);
        }
    }
}
