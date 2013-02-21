namespace BetterClassified.UI
{
    using System;
    using System.Web.UI.WebControls;
    using BetterclassifiedsCore;
    using CategorySelectorSupport;
    using Paramount.Common.UI.BaseControls;

    public class CategorySelector:ParamountCompositeControl
    {
        protected readonly DataList mainCategoryList;
        public event EventHandler OnCategoryClick;

        private void InvokeOnCategoryClick(object sender, EventArgs e)
        {
            EventHandler handler = OnCategoryClick;
            if (handler != null) handler(sender, e);
        }

        public CategorySelector()
        {
            var template = new MainCategoryTemplate();
            template.OnMainCategoryClick += OnMainCategoryClick;
            mainCategoryList = new DataList{ItemTemplate=template };
            mainCategoryList.ItemStyle.CssClass = "sideBarCategory";
        }

        void OnMainCategoryClick(object sender, EventArgs e)
        {
            InvokeOnCategoryClick(sender, e);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.Controls.Add(mainCategoryList);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.mainCategoryList.DataSource = CategoryController.GetMainParentCategories();
            this.DataBind();
        }
    }
}