using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Paramount.Utility
{
    public static class HtmlExtensions
    {
        public static string ToHtmlTable<T>(this List<T> values) where T : class
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<table>");
            if (values.Count > 0)
            {
                // Append the top header row
                builder.Append(values.First().ToTableRow(isHeader: true));

                // Append each value
                foreach (var item in values)
                {
                    builder.Append(item.ToTableRow(isHeader: false));
                }
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
    }
}
