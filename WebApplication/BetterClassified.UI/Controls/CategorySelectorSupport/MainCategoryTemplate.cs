namespace BetterClassified.UI.CategorySelectorSupport
{
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BetterclassifiedsCore;
    using BetterclassifiedsCore.DataModel;
    using BetterclassifiedsCore.ParameterAccess;

    public class MainCategoryTemplate:ITemplate
    {
        public event EventHandler OnMainCategoryClick;

        private void InvokeOnMainCategoryClick(object sender, EventArgs e)
        {
            EventHandler handler = OnMainCategoryClick;
            if (handler != null) handler(sender, e);
        }

        public void InstantiateIn(Control container)
        {
            var control = new MainCategoryTemplateControl();
            control.DataBinding += ControlDataBinding;
            control.OnItemSelect += MainCategoryClick;
            control.OnSubCategoryClick += MainCategoryClick;
            container.Controls.Add(control);
        }

         void MainCategoryClick(object sender, EventArgs e)
        {
            InvokeOnMainCategoryClick(sender,e);
        }

        static void ControlDataBinding(object sender, EventArgs e)
        {
            var control = sender as MainCategoryTemplateControl;
            if (control == null)
            {
                return;
            }

            var dataItem = control.NamingContainer as DataListItem;
            if (dataItem == null)
            {
                return;
            }

            var category = dataItem.DataItem as MainCategory;
            if (category == null)
            {
                return;
            }

            
            if (OnlineSearchParameter.Category.HasValue && OnlineSearchParameter.Category.Value == category.MainCategoryId)
            {
                control.subCategories = CategoryController.GetMainCategoriesByParent(category.MainCategoryId);
            }
            control.Text = category.Title;
            control.CategoryId = category.MainCategoryId;
            control.SeoName = category.SeoName;
        }
    }
}