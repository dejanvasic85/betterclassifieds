namespace Paramount.Services
{
    using System;
    using System.Web.Security;
    using ApplicationBlock.Logging.AuditLogging;
    using Common.DataTransferObjects.MembershipService;
    using Common.DataTransferObjects.MembershipService.Messages;
    using Common.ServiceContracts;
    using DataService;

    public class MembershipService : IMembershipService
    {
        public CreateUserResponse CreateUser(CreateUserRequest request)
        {
            using (var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                MembershipCreateStatus status;
                var user = provider.CreateUser(
                    request.Username,
                    request.Password,
                    request.Email,
                    request.PasswordQuestion,
                    request.PasswordAnswer,
                    request.IsApproved,
                    request.ProviderUserKey,
                    out status
                    );

                CreateProfileInfo(request.ClientCode, request.ApplicationName, user.UserName, request.ProfileInformation);
                return new CreateUserResponse {Membership = user, CreateStatus = status};
            }
        }

        public void CreateProfileInfo(string clientCode, string applicationName, string username,  ProfileInfo profile)
        {
            using (var provider = new RoleDataProvider(clientCode, applicationName))
            {
                if (profile== null)
                {
                    profile = new ProfileInfo {NewsletterSubscription = true, FirstName = string.Empty , LastName = string.Empty, SecondaryEmail = string.Empty };
                }
                provider.UpdateProfileInfo(username, profile);
                provider.Commit();
            }

        }

        public ChangePasswordQuestionAndAnswerResponse ChangePasswordQuestionAndAnswer(ChangePasswordQuestionAndAnswerRequest request)
        {
            using( var provider =  new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                return new ChangePasswordQuestionAndAnswerResponse
                           {
                               Result =
                                   provider.ChangePasswordQuestionAndAnswer(request.Username, request.Password,
                                                                            request.NewPasswordQuestion,
                                                                            request.NewPasswordAnswer)
                           };
            }
        }

        public void UpdateUser(UpdateUserRequest request)
        {
            using( var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                provider.UpdateUser(request.User);
            }
        }

        public ValidateUserResponse ValidateUser(ValidateUserRequest validateUserRequest)
        {
            using( var provider = new MembershipDataProvider(validateUserRequest.ClientCode, validateUserRequest.ProviderName))
            {
                return new ValidateUserResponse
                           {Success = provider.ValidateUser(validateUserRequest.Username, validateUserRequest.Password)};
            }
        }

        public ChangePasswordResponse ChangePassword(ChangePasswordRequest request)
        {
            using (var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                return new ChangePasswordResponse {Result = provider.ChangePassword(
                    request.Username,
                    request.OldPassword,
                    request.NewPassword)};
            }
        }

        public GetUserResponse GetUserById(GetUserByIdRequest request)
        {
            request.LogRequestAudit();
            var response = new GetUserResponse();
            using( var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                response.MembershipUser = provider.GetUser(request.UserId, request.UserIsOnline);
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.GetUserByIdResponse,  response);
            AuditLogManager.Log(auditResponse);
            return response;
        }

        public GetUserResponse GetUserByUsername(GetUserByUsernameRequest request)
        {
            request.LogRequestAudit();
            var response = new GetUserResponse();
            using (var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                response.MembershipUser =  provider.GetUser(request.Username, request.UserIsOnline);
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.GetUserByUsernameResponse, response);
            AuditLogManager.Log(auditResponse);
            return response;
        }

        public GetFunctionsForUserResponse GetFunctionsForUser(GetFunctionsForUserRequest request)
        {
            request.LogRequestAudit();
            var response = new GetFunctionsForUserResponse();

            using( var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Functions = provider.GetFunctionsForUseRole(request.Username, request.ApplicationName);
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.GetFunctionsForUser + ".Response", response);
            AuditLogManager.Log(auditResponse);
            return response;
        }

        public IsUserInFunctionResponse IsUserInFunction(IsUserInFunctionRequest request)
        {
            request.LogRequestAudit();
            var response = new IsUserInFunctionResponse();

            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Result = provider.IsUserInFunction(request.Username, request.FunctionName);
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.IsUserInFunction + ".Response", response);
            AuditLogManager.Log(auditResponse);
            return response;
        }

        public FunctionExistsResponse FunctionExists(FunctionExistsRequest request)
        {
            request.LogRequestAudit();
            var response = new FunctionExistsResponse();

            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Result = provider.FunctionExists(request.FunctionName);
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.FunctionExists + ".Response", response);
            AuditLogManager.Log(auditResponse);
            return response;
        }

        public void UpdateProfileInfo(UpdateProfileRequest request)
        {
            request.LogRequestAudit();

            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                provider.UpdateProfileInfo(request.Username, request.Profile);
                provider.Commit();
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.UpdateProfile + ".Response", "done");
            AuditLogManager.Log(auditResponse);
        }

        public GetProfileResponse GetProfile(GetProfileRequest request)
        {
            request.LogRequestAudit();

            var response = new GetProfileResponse();
            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Profile = provider.GetProfile(request.Username);
            }

            var auditResponse = request.ConvertToAudit(AuditTransactions.UpdateProfile + ".Response", response);
            AuditLogManager.Log(auditResponse);

            return response;
        }
    }
}