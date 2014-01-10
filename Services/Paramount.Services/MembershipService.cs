using Paramount.Betterclassifieds.DataService;

namespace Paramount.Services
{
    using System;
    using System.Web.Security;
    
    using Common.DataTransferObjects.MembershipService;
    using Common.DataTransferObjects.MembershipService.Messages;
    using Common.ServiceContracts;

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
            
            var response = new GetUserResponse();
            using( var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                response.MembershipUser = provider.GetUser(request.UserId, request.UserIsOnline);
            }

            return response;
        }

        public GetUserResponse GetUserByUsername(GetUserByUsernameRequest request)
        {
            
            var response = new GetUserResponse();
            using (var provider = new MembershipDataProvider(request.ClientCode, request.ProviderName))
            {
                response.MembershipUser =  provider.GetUser(request.Username, request.UserIsOnline);
            }

            return response;
        }

        public GetFunctionsForUserResponse GetFunctionsForUser(GetFunctionsForUserRequest request)
        {
            
            var response = new GetFunctionsForUserResponse();

            using( var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Functions = provider.GetFunctionsForUseRole(request.Username, request.ApplicationName);
            }

            return response;
        }

        public IsUserInFunctionResponse IsUserInFunction(IsUserInFunctionRequest request)
        {
            
            var response = new IsUserInFunctionResponse();

            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Result = provider.IsUserInFunction(request.Username, request.FunctionName);
            }

            return response;
        }

        public FunctionExistsResponse FunctionExists(FunctionExistsRequest request)
        {
            
            var response = new FunctionExistsResponse();

            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Result = provider.FunctionExists(request.FunctionName);
            }

            return response;
        }

        public void UpdateProfileInfo(UpdateProfileRequest request)
        {
            

            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                provider.UpdateProfileInfo(request.Username, request.Profile);
                provider.Commit();
            }

        }

        public GetProfileResponse GetProfile(GetProfileRequest request)
        {
            

            var response = new GetProfileResponse();
            using (var provider = new RoleDataProvider(request.ClientCode, request.ApplicationName))
            {
                response.Profile = provider.GetProfile(request.Username);
            }


            return response;
        }
    }
}