namespace Paramount.DSL.UIController.ViewObjects
{
    public class DslDocumentCategoryView
    {
        public int DocumentCategoryId { get; set; }

        public DslDocumentCategoryTypeView Code { get; set; }

        public string Title { get; set; }

        public int? ExpiryPurgeDays { get; set; }

        public string[] AcceptedFileTypes { get; set; }

        public int? MaximumFileSize { get; set; }
    }
}
