using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Paramount.Utility
{
    public static class GetControlString
    {
        public static string RenderControl(Control control)
        {
            var tw = new StringWriter();

            // Simple rendering - just write the control to the text writer
            // works well for single controls without containers
            var writer = new Html32TextWriter(tw);
            control.RenderControl(writer);
            writer.Close();

            return tw.ToString();
        }

        /// <summary>
        /// Renders a control dynamically by creating a new Page and Form
        /// control and then adding the control to be rendered to it.        
        /// </summary>
        /// <remarks>
        /// This routine works to render most Postback controls but it
        /// has a bit of overhead as it creates a separate Page instance        
        /// </remarks>
        /// <param name="control">The control that is to be rendered</param>
        /// <param name="useDynamicPage">if true forces a Page to be created</param>
        /// <returns>Html or empty</returns>
        public static string RenderControl(Control control, bool useDynamicPage)
        {
            if (!useDynamicPage)
                return RenderControl(control);

            const string strBeginRenderControlBlock = "<!-- BEGIN RENDERCONTROL BLOCK -->";
            const string strEndRenderControlBlock = "<!-- End RENDERCONTROL BLOCK -->";

            var tw = new StringWriter();

            // Create a page and form so that postback controls work          
            var page = new Page();
            page.EnableViewState = false;

            var form = new HtmlForm();
            form.ID = "__t";
            page.Controls.Add(form);

            // Add placeholder to strip out so we get just the control rendered
            // and not the <form> tag and viewstate, postback script etc.
            form.Controls.Add(new LiteralControl(strBeginRenderControlBlock + "."));
            form.Controls.Add(control);
            form.Controls.Add(new LiteralControl("." + strEndRenderControlBlock));

            HttpContext.Current.Server.Execute(page, tw, true);

            string Html = tw.ToString();

            // Strip out form and ViewState, Event Validation etc.
            int at1 = Html.IndexOf(strBeginRenderControlBlock);
            int at2 = Html.IndexOf(strEndRenderControlBlock);
            if (at1 > -1 && at2 > at1)
            {
                Html = Html.Substring(at1 + strBeginRenderControlBlock.Length);
                Html = Html.Substring(0, at2 - at1 - strBeginRenderControlBlock.Length);
            }

            return Html;
        }
    }
}