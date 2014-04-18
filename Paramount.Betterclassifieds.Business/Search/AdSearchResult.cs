namespace Paramount.Betterclassifieds.Business.Search
{
    public class AdSearchResult
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] ImageUrls { get; set; }
        public string CategoryName { get; set; }
        public string[] Publications { get; set; }

        public string TitleSlug
        {
            get { return Slug.Create(true, Title); }
        }

        // These might just be temporary
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}