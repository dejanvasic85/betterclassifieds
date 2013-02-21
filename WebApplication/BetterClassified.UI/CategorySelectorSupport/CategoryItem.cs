namespace BetterClassified.UI.CategorySelectorSupport
{
    using System;
    using System.Web.UI.WebControls;
    using BaseControl;
    using Paramount.Common.UI.BaseControls;

    public class CategoryItem:ParamountCompositeControl
    {
        protected readonly LinkButton hyperlink;

        public  event EventHandler OnItemSelect;

        private void InvokeOnItemSelect(EventArgs e)
        {
            var handler = OnItemSelect;
            if (handler != null) handler(this, e);
        }

        public CategoryItem()
        {
            hyperlink = new LinkButton();
            hyperlink.Click += HyperlinkServerClick;
        }

        void HyperlinkServerClick(object sender, EventArgs e)
        {
            InvokeOnItemSelect(e);
        }

        protected override void CreateChildControls()
        {
            Controls.Add(hyperlink);
        }

        public string Text
        {
            get { return(string) ViewState["Text"]; }
            set
            {
                ViewState["Text"] = value;
                this.hyperlink.Text = value;
            }
        }

        public int? CategoryId
        {
            get { return (ViewState["CategoryId"] as int?); }
            set { ViewState["CategoryId"] = value; }
        }

    }
}