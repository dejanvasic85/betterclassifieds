namespace Paramount.Betterclassifieds.Business
{
    public enum RegistrationConfirmationResult
    {
        Successful,
        TokenNotCorrect,
        TokenExpired,
        RegistrationExpired,
        RegistrationDoesNotExist
    }
}