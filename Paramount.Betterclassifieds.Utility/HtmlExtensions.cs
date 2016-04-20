using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Paramount
{
    public static class HtmlExtensions
    {
        public static string ToHtmlTable<T>(this List<T> values) where T : class
        {
            if (values.Count == 0)
                return string.Empty;

            StringBuilder builder = new StringBuilder();
            builder.Append("<table>");

            // Append the top header row
            builder.Append(values.First().ToTableRow(isHeader: true));

            // Append each value
            foreach (var item in values)
            {
                builder.Append(item.ToTableRow(isHeader: false));
            }
            builder.Append("</table>");
            return builder.ToString();
        }

        private static string ToTableRow<T>(this T item, bool isHeader)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<tr>");
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (isHeader)
                {
                    builder.AppendFormat("<th>{0}</th>", property.Name);
                }
                else
                {
                    builder.AppendFormat("<td>{0}</td>", property.GetValue(item, null));
                }
            }
            builder.Append("</tr>");
            return builder.ToString();
        }

        public static string ToHtmlTable<TKey, TValue>(this Dictionary<TKey, TValue> values)
        {
            if (values.Count == 0)
                return string.Empty;

            StringBuilder builder = new StringBuilder();
            builder.Append("<table>");

            foreach (var item in values)
            {
                builder.Append("<tr>");
                builder.AppendFormat("<td>{0}</td><td>{1}</td>", item.Key, item.Value);
                builder.Append("</tr>");
            }

            builder.Append("</table>");
            return builder.ToString();
        }

        /// <summary>
        /// Uses the html agility pack to convert the html string to plaintext 
        /// </summary>
        public static string FromHtmlToPlaintext(this string html)
        {
            // create whitespace between html elements, so that words do not run together
            html = html.Replace(">", "> ");

            // parse html
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // strip html decoded text from html
            string text = HttpUtility.HtmlDecode(doc.DocumentNode.InnerText);

            // replace all whitespace with a single space and remove leading and trailing whitespace
            var result = Regex.Replace(text, @"\s+", " ").Trim();

            return result;
        }
        
    }
}
