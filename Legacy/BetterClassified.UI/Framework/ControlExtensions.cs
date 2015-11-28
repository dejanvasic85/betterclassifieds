namespace BetterClassified.UI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using BetterclassifiedsCore.DataModel;
    using Telerik.Web.UI;

    public static class ControlExtensions
    {
        public static Panel DivWrap(this Control control, string cssClass)
        {
            var panel = new Panel{CssClass = cssClass};
            panel.Controls.Add(control);
            return panel;
        }

        public static void BindLineAdColours(this RadColorPicker radColourPicker, IList<LineAdColourCode> lineAdColourCodes)
        {
            for (int i = 0; i < lineAdColourCodes.Count; i++)
            {
                radColourPicker.Items.Add(new ColorPickerItem
                {
                    Index = i,
                    Title = lineAdColourCodes[i].LineAdColourName,
                    Value = ColorTranslator.FromHtml(lineAdColourCodes[i].ColourCode)
                });
            }
        }

        public static bool Contains(this RadColorPicker radColourPicker, string colourCode)
        {
            return radColourPicker.Items
                .Cast<ColorPickerItem>()
                .Any(item => string.Compare(ColorTranslator.ToHtml(item.Value), colourCode, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public static void AddCssResource(this Page page, Type serverControlType, string resourceName)
        {
            var resourceUrl = page.ClientScript.GetWebResourceUrl(serverControlType, resourceName);
            var includeControl = new LiteralControl(string.Format("<link rel='stylesheet' text='text/css' href='{0}' />", resourceUrl));
            page.Header.Controls.Add(includeControl);
        }
    }
}