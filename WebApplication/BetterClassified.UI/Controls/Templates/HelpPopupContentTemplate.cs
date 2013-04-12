using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BetterClassified.UI
{
    public class HelpPopupContentTemplate : ITemplate
    {
        public HelpPopupContentTemplate(string content)
        {
            this.Content = content;
        }

        public string Content
        {
            get;
            private set;
        }
        
        public void InstantiateIn(Control container)
        {
            var label = new Label { Text = Content };
            container.Controls.Add(label);
        }
    }
}
