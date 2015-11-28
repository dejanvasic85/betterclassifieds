using System.Web.UI;
using System;
using System.Web.UI.WebControls;

namespace BetterClassified.UI.Dsl
{
    public class ImageThumbCommandTemplate : ITemplate
    {
        public event EventHandler ThumbCommandClick;

        public void InstantiateIn(Control container)
        {
            var control = new ImageThumbCommand { CommandText = "Remove" };
            control.CommandClick += ThumbClick;
            control.DataBinding += ThumbListBinding;
            container.Controls.Add(control);
        }

        #region Event Handling

        void ThumbClick(object sender, EventArgs e)
        {
            InvokeOnThumbCommandClick(sender, e);
        }

        private void InvokeOnThumbCommandClick(object sender, EventArgs e)
        {
            EventHandler handler = ThumbCommandClick;
            if (handler != null) handler(sender, e);
        }

        private static void ThumbListBinding(object sender, EventArgs e)
        {
            var control = sender as ImageThumbCommand;
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

        #endregion
    }
}