namespace BetterClassified.UI
{
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public static class Converter
    {
        public static Panel DivWrap(this Control control)
        {
            var panel = new Panel();
            panel.Controls.Add(control);
            return panel;
        }

        public static Panel DivWrap(this Control control, string cssClass)
        {
            var panel = new Panel{CssClass = cssClass};
            panel.Controls.Add(control);
            return panel;
        }
        
    }
}