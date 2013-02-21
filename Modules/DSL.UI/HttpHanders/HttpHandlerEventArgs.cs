using System;
using System.Web;

namespace Paramount.DSL.UI.HttpHanders
{
    public class HttpHandlerEventArgs:EventArgs
    {
        public HttpContext Context { get; private set; }

        public HttpHandlerEventArgs(HttpContext context, string groupingId )
        {
            Context = context;
            GroupingId = groupingId;
        }

        public string GroupingId { get; private set; }
    }
}