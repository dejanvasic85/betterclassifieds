namespace Paramount.Betterclassifieds.Business
{
    public class AdImage
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }

        public override string ToString()
        {
            return this.ImageUrl;
        }
    }
}