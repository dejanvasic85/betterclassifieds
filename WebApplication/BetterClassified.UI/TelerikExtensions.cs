using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using BetterclassifiedsCore.DataModel;
using System.Drawing;

namespace BetterClassified.UI
{
    public static class TelerikExtensions
    {
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
            foreach (ColorPickerItem item in radColourPicker.Items)
            {
                if (string.Compare(ColorTranslator.ToHtml(item.Value), colourCode) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
