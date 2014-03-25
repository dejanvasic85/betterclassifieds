namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class NewRegistration : IDocType
    {
        public string DocumentTemplate
        {
            get { return GetType().Name; }
        }

        [Placeholder("FirstName")]
        public string FirstName { get; set; }

        [Placeholder("LastName")]
        public string LastName { get; set; }
    }
}