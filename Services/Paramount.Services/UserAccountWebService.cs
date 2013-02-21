namespace Paramount.Services
{
    using System;
    using System.Linq;
    using Common.DataTransferObjects.UserAccountService;
    using Common.DataTransferObjects.UserAccountWebService.Messages;
    using Common.ServiceContracts;
    using DataService.LinqObjects;

    public class UserAccountWebService : IUserAccountWebService
    {
        public GetNewsletterUsersResponse GetNewsletterUsers(GetNewsletterUsersRequest request)
        {
            var result = UserMembershipDataContext.GetSubscriptionUser("BetterClassified", request.UsernameMatch, request.IncludeAll, request.PageIndex, request.PageSize);

            var accounts = from item in result.Data
                           select
                               new UserAccountProfile
                               {
                                   Email = item.Email,
                                   UserId = item.UserId,
                                   Username = item.aspnet_User.UserName,
                                   CreateDate = item.CreateDate,
                                   IsApproved = item.IsApproved,
                                   IsLockedOut = item.IsLockedOut
                               };

            return new GetNewsletterUsersResponse
            {
                Profiles = accounts.ToList(),
                TotalCount = result.TotalCount
            };
        }

        public void UnsubscribUser(UnSubscribeUserRequest request)
        {
            UserMembershipDataContext.UnSubscribeUser(request.UserName, "BetterClassified");
        }
  

    }
}
