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
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (data.Model.ToString().IsNullOrEmpty())
                return MvcHtmlString.Empty;

            var builder = new StringBuilder();
            builder.Append("<tr>");
            builder.AppendFormat("<td><span class='glyphicon glyphicon-{0}'><span></td>", glyph);
            builder.AppendFormat("<td><span id='{0}'>{1}</span></td>", data.PropertyName, data.Model);
            builder.Append("</tr>");

            return new MvcHtmlString(builder.ToString());
        }
    }
}
