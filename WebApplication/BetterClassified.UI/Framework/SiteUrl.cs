using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BetterClassified.UI
{

    /// <summary>
    /// Simple class for generating an outgoing absolute URL's for stuff like Facebook and Twitter (and other)
    /// </summary>
    public class SiteUrl
    {
        public string Url { get; private set; }

        /// <summary>
        /// Accepts a relative Server URL such as ~/Image/Handler.ashx
        /// </summary>
        public SiteUrl(string relativeServerUrl)
        {
            this.Url = relativeServerUrl;
        }

        public string ToAbsoluteUrl(HttpRequest request)
        {
            var currentUrl = Url;
            if (Url.StartsWith("~"))
            {
                currentUrl = Url.TrimStart('~');
            }

            if (request.ApplicationPath.EqualTo("/"))
            {
                return string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Host, currentUrl);
            }

            return string.Format("{0}://{1}{2}{3}", request.Url.Scheme, request.Url.Host, request.ApplicationPath, currentUrl);
        }

        public static implicit operator String(SiteUrl siteUrl)
        {
            return siteUrl.Url;
        }
    }
}
