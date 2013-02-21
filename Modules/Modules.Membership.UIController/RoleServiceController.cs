namespace Paramount.Membership.UIController
{
    using System;
    using System.Web.Security;
    using Common.DataTransferObjects.MembershipService.Messages;
    using Services.Proxy;

    public class RoleServiceController:RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new IsUserInFunctionRequest { Username = username, FunctionName = roleName  };
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.IsUserInFunction(request).Result;
        }

        public override string[] GetRolesForUser(string username)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new GetFunctionsForUserRequest{ Username = username};
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.GetFunctionsForUser(request).Functions;
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var groupdingId = Guid.NewGuid().ToString();
            var request = new FunctionExistsRequest { FunctionName = roleName  };
            request.SetBaseRequest(groupdingId);
            return WebServiceHostManager.MembershipServiceClient.FunctionExists(request).Result;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get; set;
        }
    }
}