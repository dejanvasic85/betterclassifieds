namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class AdEnquiry : IDocType
    {
        public string DocumentType
        {
            get { return GetType().Name; }
        }

        [Placeholder("adNumber")]
        public string AdNumber { get; set; }
    }
}