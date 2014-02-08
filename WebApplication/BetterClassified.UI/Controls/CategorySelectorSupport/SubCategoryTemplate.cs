using System.Web.UI;
namespace BetterClassified.UI.CategorySelectorSupport
{
    using System;
    using System.Web.UI.WebControls;
    using BetterclassifiedsCore.DataModel;

    public class SubCategoryTemplate:ITemplate
    {
        public event EventHandler OnSubCategoryClick;

        private void InvokeOnSubCategoryClick(object sender,EventArgs e)
        {
            EventHandler handler = OnSubCategoryClick;
            if (handler != null) handler(sender , e);
        }

        public void InstantiateIn(Control container)
        {
            var subCategoryControl = new SubCategoryTemplateControl();
            subCategoryControl.DataBinding += SubCategoryControlDataBinding;
            subCategoryControl.OnItemSelect += SubCategory;
            container.Controls.Add(subCategoryControl);
        }

        void SubCategory(object sender, EventArgs e)
        {
            InvokeOnSubCategoryClick(sender, e);
        }

        static void SubCategoryControlDataBinding(object sender, EventArgs e)
        {
            var control = sender as SubCategoryTemplateControl;
            if(control == null)
            {
                return;
            }

            var dataItem = control.NamingContainer as DataListItem;
            if(dataItem == null )
            {
                return;
            }

            var category = dataItem.DataItem as MainCategory;
            if(category == null)
            {
                return;
            }

            control.Text = " - " + category.Title;
            control.CategoryId  = category.MainCategoryId;
            control.ParentCategoryId = category.ParentId;
            control.SeoName = category.SeoName;
        }
    }
}