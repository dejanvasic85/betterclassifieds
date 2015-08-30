namespace Paramount.Betterclassifieds.Business
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string OnlineAdTag { get; set; }
        public string SeoName { get; set; }
        public bool IsOnlineOnly { get; set; }
        public string CategoryAdType { get; set; }
    }
}