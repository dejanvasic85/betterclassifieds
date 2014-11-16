namespace Paramount.Betterclassifieds.Business.Broadcast
{
    public class AdEnquiryTemplate : IDocType
    {
        public string DocumentType
        {
            get { return "AdEnquiry"; }
        }

        [Placeholder("adNumber")]
        public string AdNumber { get; set; }
        [Placeholder("name")]
        public string Name { get; set; }
        [Placeholder("email")]
        public string Email { get; set; }
      

    }
}