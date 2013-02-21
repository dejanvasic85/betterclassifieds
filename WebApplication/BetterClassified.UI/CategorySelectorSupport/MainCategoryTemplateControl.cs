namespace BetterClassified.UI.CategorySelectorSupport
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using BaseControl;
    using BetterclassifiedsCore;
    using BetterclassifiedsCore.DataModel;

    public class MainCategoryTemplateControl : CategoryItem
    {
        public event EventHandler OnSubCategoryClick;
        public  List<MainCategory> subCategories;


        private void InvokeOnCategoryClick(object sender, EventArgs e)
        {
            var handler = OnSubCategoryClick;
            if (handler != null) handler(sender, e);
        }

        protected DataList subList;

        public MainCategoryTemplateControl()
        {
            var template = new SubCategoryTemplate();
            template.OnSubCategoryClick += TemplateOnSubCategoryClick;
            subList = new DataList
                          {
                              ItemTemplate = template,
                              CssClass = "sub-category"
                          };
            this.OnItemSelect += OnParentCategorySelect;
            subCategories = new List<MainCategory>();
        }

        protected override void  OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.subList.DataSource = this.subCategories;
            this.subList.DataBind();
        }
        void TemplateOnSubCategoryClick(object sender, EventArgs e)
        {
            InvokeOnCategoryClick(sender, e);
        }

        protected override void CreateChildControls()
        {
            this.Controls.Add(this.hyperlink);
            this.Controls.Add(this.subList);
        }

        void OnParentCategorySelect(object sender, EventArgs e)
        {
            //var control = sender as MainCategoryTemplateControl;
            //if (control != null && control.CategoryId.HasValue)
            //{
            //    this.subList.DataSource = CategoryController.GetMainCategoriesByParent(control.CategoryId.Value);
            //    this.subList.DataBind();
            //    this.subList.Visible = true;
            //}
        }
    }
}