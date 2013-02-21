namespace Paramount.Common.UI
{
    using System.Collections.Generic;
    using BaseControls;

    public class ScriptsControl:ParamountCompositeControl
    {
        private static IEnumerable<string> GetScripts()
        {
            //add new scrip here
            return new List<string>
                       {
                           "Paramount.Common.UI.JavaScript.jquery-1.4.min.js",
                           "Paramount.Common.UI.JavaScript.json2.js"
                       };
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            foreach (var item in GetScripts())
            {
                Page.ClientScript.RegisterClientScriptResource(GetType(), item);
            }
        }
    }
}