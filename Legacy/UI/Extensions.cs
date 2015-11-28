using System.Collections.Generic;

namespace Paramount.Common.UI
{
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Web;
    using System.Web.UI;

    public static class Extensions
    {
        public static Panel DivWrapLabel(this Control control)
        {
            var panel = control.DivWrap();
            panel.CssClass = "formSectionTitle";
            return panel;
        }

        public static Panel DivWrapValue(this Control control)
        {
            var panel = control.DivWrap();
            panel.CssClass = "formSectionValue";
            return panel;
        }

        public static Panel DivWrapLabelValue(this Control control, string lableText)
        {
            var panel = new Panel() { CssClass = "label-value" };

            var label = new Label() { };
            label.Text = lableText;
            var labelPanel = label.DivWrapLabel();

            panel.Controls.Add(labelPanel);
            panel.Controls.Add(control.DivWrapValue());

            return panel;
        }

        public static Panel DivWrapLabelValue(this Control control, string lableText, Control validation)
        {
            var panel = control.DivWrapLabelValue(lableText);
            panel.Controls.Add(validation);
            return panel;
        }

        public static Panel DivWrapLabelValue(this Control control, string lableText, List<Control> validations)
        {
            var panel = control.DivWrapLabelValue(lableText);
            foreach (var validation in validations)
            {
                panel.Controls.Add(validation);    
            }
            
            return panel;
        }

        public static Panel DivWrap(this Control control)
        {
            var panel = new Panel();
            panel.Controls.Add(control);
            return panel;
        }

        public static Panel DivWrap(this Control control, string cssClass)
        {
            var panel = new Panel { CssClass = cssClass };
            panel.Controls.Add(control);
            return panel;
        }

    }
}
