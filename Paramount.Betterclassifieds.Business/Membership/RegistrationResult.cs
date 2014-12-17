namespace Paramount.Betterclassifieds.Business
{
    public class RegistrationResult
    {
        public RegistrationModel Registration { get; private set; }
        public bool RequiresConfirmation { get; private set; }

        public RegistrationResult(RegistrationModel registration, bool requiresConfirmation)
        {
            Registration = registration;
            RequiresConfirmation = requiresConfirmation;
        }
    }
}