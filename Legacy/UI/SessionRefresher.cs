namespace Paramount.Common.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.UI;
    using Paramount.Common.UI.BaseControls;

    public class SessionRefresher : ParamountCompositeControl
    {
        public int TimeOutPeriod { get; set; }

        [UrlProperty]
        public string SessionUrl { get; set; }

        protected override void OnPreRender(System.EventArgs e)
        {

            Page.ClientScript.RegisterClientScriptBlock(GetType(), "sesReqUrl", "var sesReqUrl='" + ResolveUrl(SessionUrl) + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "sesReqTime", "var sesReqTime=" + TimeOutPeriod + ";", true);
            Page.ClientScript.RegisterClientScriptResource(GetType(), "Paramount.Common.UI.JavaScript.session-refresher.js");

        }
    }
}
