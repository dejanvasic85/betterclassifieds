using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Paramount.ApplicationBlock.Mvc.HtmlHelpers
{
    public static class AdHtmlHelpers
    {
        public static MvcHtmlString AdContactDetail<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string glyph)
        {
            var data = (string)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;

            if (data.IsNullOrEmpty())
                return MvcHtmlString.Empty;

            var builder = new StringBuilder();
            builder.Append("<tr>");
            builder.AppendFormat("<td><span class='glyphicon glyphicon-{0}'><span></td>", glyph);
            builder.AppendFormat("<td><span id='contactName'>{0}</span></td>", data);
            builder.Append("</tr>");

            return new MvcHtmlString(builder.ToString());
        }
    }
}
