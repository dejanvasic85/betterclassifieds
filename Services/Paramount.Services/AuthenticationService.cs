namespace Paramount.Services
{
    using System;
    using System.Web.Security;
    using System.ServiceModel;
    using System.Runtime.Serialization;
    using Common.DataTransferObjects.UserAccountService.Messages;
    using Common.DataTransferObjects.UserAccountService;
    using Paramount.ApplicationBlock.Membership;
    using Paramount.DataService.LinqObjects;

    public class AuthenticationService
    {
        private static MembershipProvider ParamountMembership
        {
            get { return Membership.Providers[AuthorisationProviderKey.ParamountMembership.ToString()]; }

        }

        private static string GetDomainAndUsername(string domain, string username)
        {
            return domain + @"\" + username;
        }

        private void GetDomainAndUsername(string domainAndUsername, out string domain, out string username)
        {
            var stringpair = domainAndUsername.Split(new[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            if (stringpair.Length == 0)
            {
                domain = string.Empty;
                username = string.Empty;
                return;
            }
            if (stringpair.Length == 1)
            {
                username = stringpair[0];
                domain = string.Empty;
                return;
            }
            domain = stringpair[0];
            username = stringpair[1];
        }

        [OperationContract]
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            MembershipCreateStatus createStatus;
            var membership = ParamountMembership.CreateUser(GetDomainAndUsername(request.ClientCode, request.Username), request.Password, request.Email, request.SecurityQuestion,
                                           request.SecurityAnswer,
                                           request.IsApproved, request.ProviderUserKey, out createStatus);
            var response = new CreateUserResponse(membership) { CreateStatus = createStatus };

            return response;
        }

        public bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        [OperationContract]
        public GetPasswordResponse GetPassword(GetPasswordRequest request)
        {
            return new GetPasswordResponse
            {
                Password =
                    ParamountMembership.GetPassword(
                        GetDomainAndUsername(request.ClientCode, request.Username), request.Answer)
            };
        }

        [OperationContract]
        public GetResultResponse ChangePassword(ChangePasswordRequest request)
        {
            return new GetResultResponse
            {
                Result =
                    ParamountMembership.ChangePassword(
                        GetDomainAndUsername(request.ClientCode, request.Username), request.OldPassword,
                        request.NewPassword)
            };
        }

        [OperationContract]
        public GetPasswordResponse ResetPassword(GetPasswordRequest request)
        {
            return new GetPasswordResponse { Password = ParamountMembership.ResetPassword(GetDomainAndUsername(request.ClientCode, request.Username), request.Answer) };
        }

        [OperationContract]
        public void UpdateUser(UpdateUserRequest request)
        {
            ParamountMembership.UpdateUser(request.User);
        }

        [OperationContract]
        public ValidateUserResponse ValidateUser(ValidateUserRequest request)
        {
            return new ValidateUserResponse
            {
                Success = ParamountMembership.ValidateUser(GetDomainAndUsername(request.ClientCode, request.Username), request.Password)
            };
        }

        public bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        [OperationContract]
        public ParamountMembershipUserResponse GetUser(GetUserByIdRequest request)
        {
            var user = ParamountMembership.GetUser(request.UserId, request.UserIsOnline);

            return new ParamountMembershipUserResponse
            {
                User = new ParamountMembershipUser(request.ClientCode, user)
            };
        }

        [OperationContract]
        public ParamountMembershipUserResponse GetUser(GetUserByUsernameRequest request)
        {
            var user = ParamountMembership.GetUser(request.Username, request.UserIsOnline);

            return new ParamountMembershipUserResponse
            {
                User = new ParamountMembershipUser(request.ClientCode, user)
            };
        }

        public GetUserNameByEmailResponse GetUserNameByEmail(GetUserNameByEmailRequest request)
        {
            throw new NotImplementedException();
        }

        [OperationContract]
        public GetResultResponse DeleteUser(DeleteUserRequest request)
        {
            return new GetResultResponse
            {
                Result =
                    ParamountMembership.DeleteUser(
                        GetDomainAndUsername(request.ClientCode, request.Username),
                        request.DeleteRelatedData)
            };
        }

        [OperationContract]
        public GetUsersResponse GetAllUsers(GetAllUsersRequest request)
        {
            var users = UserMembershipDataContext.GetAllUsers(request.ClientCode, request.PageIndex, request.PageSize);

            var membershipCollection = new MembershipUserCollection();

            foreach (var item in users.Data)
            {
                membershipCollection.Add(ParamountMembership.GetUser(item.UserId, false));
            }

            var response = new GetUsersResponse { Membership = membershipCollection, TotalRecord = users.TotalCount };
            return response;
        }

        public int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        [OperationContract]
        public GetUsersResponse FindUsersByName(FindUsersByNameRequest request)
        {
            int totalCount;
            var list = ParamountMembership.FindUsersByName(request.UsernameToMatch, request.PageIndex, request.PageSize,
                                               out totalCount);
            return new GetUsersResponse { Membership = list, TotalRecord = totalCount };
        }

        public MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public string ApplicationName { get; set; }

        public int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        [DataMember]
        public bool RequiresUniqueEmail
        {
            get { return ParamountMembership.RequiresUniqueEmail; }
        }

        public MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}
