namespace Paramount.Betterclassifieds.Business.Search
{
    public class CategorySearchResult
    {
        public int MainCategoryId { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }

        public bool IsOnlineOnly { get; set; }
        public string CategoryAdType { get; set; }
    }
}