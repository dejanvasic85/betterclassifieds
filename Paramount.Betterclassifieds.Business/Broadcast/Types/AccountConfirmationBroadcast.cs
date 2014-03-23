namespace Paramount.Betterclassifieds.Business
{
    public class AccountConfirmationBroadcast : Broadcast
    {
        public override string TemplateName
        {
            get { return "AccountConfirmation"; }
        }

        [Placeholder("FirstName")]
        public string FirstName { get; set; }

        [Placeholder("LastName")]
        public string LastName { get; set; }
    }
}