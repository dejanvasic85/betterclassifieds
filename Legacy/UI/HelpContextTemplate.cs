namespace Paramount.Common.UI
{
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public class HelpContextTemplate : ITemplate
    {
        public string Text { get; set; }
        public void InstantiateIn(Control container)
        {
            container.Controls.Add(new Label { Text = Text, CssClass = "help-context-text" });
        }
    }
}
