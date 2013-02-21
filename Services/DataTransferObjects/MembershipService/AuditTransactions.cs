namespace Paramount.Common.DataTransferObjects.MembershipService
{
    public static class AuditTransactions
    {
        public static string CreateUserRequest = "CreateUser";
        public static string CreateUserResponse = "CreateUser.Response";
        public static string ChangePasswordQuestionAndAnswerRequest = "ChangePasswordQuestionAndAnswer";
        public static string ChangePasswordQuestionAndAnswerResponse = "ChangePasswordQuestionAndAnswer.Response";
        public static string ValidateUserRequest = "ValidateUser";
        public static string ValidateUserResponse = "ValidateUser.Response";
        public static string UpdateUserRequest = "UpdateUser";
        public static string UpdateUserResponse = "UpdateUser.Response";
        public static string ChangePasswordRequest = "ChangePassword";
        public static string ChangePasswordResponse = "ChangePassword.Response";
        public static string GetUserByIdRequest = "GetUserById";
        public static string GetUserByIdResponse = "GetUserById.Response";
        public static string GetUserByUsernameRequest = "GetUserByUsername";
        public static string GetUserByUsernameResponse = "GetUserByUsername.Response";
        public static string GetFunctionsForUser = "GetFunctionsForUser";
        public static string IsUserInFunction = "IsUserInFunction";
        public static string FunctionExists = "FunctionExists";
        public static string UpdateProfile = "UpdateProfile";
        public static string GetProfile = " GetProfile";
    }
}