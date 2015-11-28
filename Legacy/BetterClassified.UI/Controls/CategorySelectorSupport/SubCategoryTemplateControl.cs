namespace BetterClassified.UI.CategorySelectorSupport
{
    public class SubCategoryTemplateControl : CategoryItem
    {
        public int? ParentCategoryId
        {
            get
            {
                return ViewState["ParentCategoryId"] as int?;
            }
            set
            {
                ViewState["ParentCategoryId"] = value;
            }
        }
    }
}