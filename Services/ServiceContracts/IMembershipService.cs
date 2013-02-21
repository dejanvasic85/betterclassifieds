namespace Paramount.Common.ServiceContracts
{
    using System.ServiceModel;
    using System.Web.Security;
    using DataTransferObjects.MembershipService.Messages;

    [ServiceContract]
    public interface IMembershipService
    {
        [OperationContract]
        CreateUserResponse CreateUser(CreateUserRequest request);

        [OperationContract]
        ChangePasswordQuestionAndAnswerResponse ChangePasswordQuestionAndAnswer(
            ChangePasswordQuestionAndAnswerRequest request);

        [OperationContract]
        void UpdateUser(UpdateUserRequest request);

        [OperationContract]
        ValidateUserResponse ValidateUser(ValidateUserRequest validateUserRequest);

        [OperationContract]
        ChangePasswordResponse ChangePassword(ChangePasswordRequest request);

        [OperationContract]
        GetUserResponse GetUserById(GetUserByIdRequest request);

        [OperationContract]
        GetUserResponse GetUserByUsername(GetUserByUsernameRequest request);

        [OperationContract]
        GetFunctionsForUserResponse GetFunctionsForUser(GetFunctionsForUserRequest request);

        [OperationContract]
        IsUserInFunctionResponse IsUserInFunction(IsUserInFunctionRequest request);

        [OperationContract]
        FunctionExistsResponse FunctionExists(FunctionExistsRequest request);

        [OperationContract]
        void UpdateProfileInfo(UpdateProfileRequest request);

        [OperationContract]
        GetProfileResponse GetProfile(GetProfileRequest request);
        
    }
}