using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using BetterClassified.UIController;
using System.Drawing;

namespace BetterClassified.UI
{
    public class LineAdColourPicker : RadColorPicker
    {
        private LineAdController _controller;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _controller = new LineAdController();

            // Set the properties as required and Databind to the required colours
            this.Columns = 4;
            this.Preset = ColorPreset.None;
            this.Width = Unit.Pixel(80);
            this.PreviewColor = false;
            this.ShowEmptyColor = false;
            this.ShowIcon = true;

            dataBindColours();
        }

        private void dataBindColours()
        {
            var colourCodes = _controller.GetLineAdColourCodes();
            this.BindLineAdColours(colourCodes);
        }

        public string SelectedHtmlColour
        {
            get
            {
                return ColorTranslator.ToHtml(this.SelectedColor);
            }
            set
            {
                this.SelectedColor = ColorTranslator.FromHtml(value);
            }
        }
    }
}
