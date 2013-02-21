namespace Paramount.Common.UI
{
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class ListControlItem
    {
        public Label ControlLabel { get; set; }
        public Control Control { get; set; }
        public bool HasValidator { get; set; }
        public BaseValidator Validator { get; set; }
        public string CssClass { get; set; }

        public ListControlItem()
        {
            this.ControlLabel = new Label();
            this.Control = new Control();
        }
    }
}
