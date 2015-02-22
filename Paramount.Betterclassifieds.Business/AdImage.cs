namespace Paramount.Betterclassifieds.Business
{
    public class AdImage
    {
        public AdImage()
        {
            
        }

        public AdImage(string documentId)
        {
            this.DocumentId = documentId;
        }

        public string DocumentId { get; set; }
        public bool IsMain { get; set; }

        public override string ToString()
        {
            return this.DocumentId;
        }
    }
}