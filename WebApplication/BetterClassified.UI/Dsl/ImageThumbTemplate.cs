using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterClassified.UI.Dsl
{
    public class ImageThumbTemplate : ITemplate
    {
        public event EventHandler ThumbImageClick;

        private void InvokeOnThumbImageClick(object sender, EventArgs e)
        {
            EventHandler handler = ThumbImageClick;
            if (handler != null) handler(sender, e);
        }

        public void InstantiateIn(Control container)
        {
            var control = new ImageThumb();
            control.DataBinding += ImageGalleryThumbBinding;
            control.ImageSelect += ThumbClick;
            container.Controls.Add(control);
        }

        void ThumbClick(object sender, EventArgs e)
        {
            InvokeOnThumbImageClick(sender, e);
        }

        private static void ImageGalleryThumbBinding(object sender, EventArgs e)
        {
            var control = sender as ImageThumb;
            if (control == null)
            {
                return;
            }

            var dataItem = control.NamingContainer as DataListItem;
            if (dataItem == null)
            {
                return;
            }

            var documentId = dataItem.DataItem.ToString();
            control.DocumentId = documentId;
        }
    }
}