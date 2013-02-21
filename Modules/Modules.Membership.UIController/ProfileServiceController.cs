namespace Paramount.Membership.UIController
{
    using System;
    using System.Configuration;
    using Common.DataTransferObjects.MembershipService;
    using Common.DataTransferObjects.MembershipService.Messages;
    using Services.Proxy;

    public class ProfileServiceController
    {
        public static  ProfileInfo GetProfile(string username)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new GetProfileRequest() { Username = username};
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.GetProfile(request).Profile;
        }
    }
}