namespace Paramount.Common.UI
{
    using System.Collections.ObjectModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BaseControls;

    public class LayoutControl : ParamountCompositeControl
    {
        private readonly Collection<ListControlItem> layoutList;
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Ul; }
        }

        public LayoutControl()
        {
            this.layoutList = new Collection<ListControlItem>();
        }

        public void Add(Label labelControl, Control valueControl, string cssClass, BaseValidator validator)
        {
            var item = new ListControlItem
            {
                Control = valueControl,
                ControlLabel = labelControl,
                HasValidator = (validator != null),
                CssClass = cssClass,
                Validator = (validator ?? null)
            };
            this.layoutList.Add(item);
        }

        public void Add(Label labelControl, Control valueControl, string cssClass)
        {
            this.Add(labelControl, valueControl, cssClass, null);
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            this.CssClass = "layout";
            foreach (var item in this.layoutList)
            {
                var valueControl = item.Control;
                var listPanel = new ListItemPanel { CssClass = item.CssClass };
                var valuePanel = new Panel { };

                item.ControlLabel.AssociatedControlID = item.Control.UniqueID;

                valuePanel.Controls.Add(valueControl);
                listPanel.Controls.Add(item.ControlLabel);



                listPanel.Controls.Add(valuePanel);

                if (item.HasValidator)
                {
                    item.Validator.ControlToValidate = valuePanel.Controls[valuePanel.Controls.Count - 1].UniqueID;
                    listPanel.Controls.Add(item.Validator);
                }
                this.Controls.Add(listPanel);

            }
        }
    }
}
