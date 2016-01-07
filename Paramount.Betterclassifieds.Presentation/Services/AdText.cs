using System;
using System.Web;

namespace Paramount.Betterclassifieds.Presentation
{
    public class AdText
    {
        public static AdText FromHtml(string html)
        {
            if (html == null)
                html = string.Empty;

            return new AdText
            {
                HtmlText = html,
                HtmlTextEncoded = HttpUtility.HtmlEncode(html).ReplaceLineBreaks("<br />"),
                Plaintext = html.ReplaceLineBreaks().FromHtmlToPlaintext()
            };

        }

        public static AdText FromHtmlEncoded(string htmlEncoded)
        {
            if (htmlEncoded == null)
                htmlEncoded = string.Empty;

            var html = HttpUtility.HtmlDecode(htmlEncoded.Replace("<br />", Environment.NewLine));
            
            return new AdText
            {
                HtmlText = html,
                HtmlTextEncoded = htmlEncoded,
                Plaintext = html.ReplaceLineBreaks().FromHtmlToPlaintext()
            };
        }

        /// <summary>
        /// Contains raw html that may be submitted by the user 
        /// </summary>
        public string HtmlText { get; private set; }

        /// <summary>
        /// Contains marked up text that would be html encoded and replaces new line characters with html breaks. Ready for @Html.Raw in the UI
        /// </summary>
        public string HtmlTextEncoded { get; private set; }

        /// <summary>
        /// Contains simple text without line breaks or any html and great for displaying in search results
        /// </summary>
        public string Plaintext { get; private set; }


        /// <summary>
        /// Returns the plaintext version by default
        /// </summary>
        public override string ToString()
        {
            return Plaintext;
        }
    }
}