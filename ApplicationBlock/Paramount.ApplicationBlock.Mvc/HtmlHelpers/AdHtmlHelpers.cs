using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Paramount.ApplicationBlock.Mvc.HtmlHelpers
{
    public static class AdHtmlHelpers
    {
        /// <summary>
        /// Renders a table row in the contact and ad details sections when viewing an ad
        /// </summary>
        public static MvcHtmlString AdContactDetail<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string glyph = "", string fontIcon = "")
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (data.Model.ToString().IsNullOrEmpty())
                return MvcHtmlString.Empty;

            var builder = new StringBuilder();
            builder.Append("<tr>");
            if (glyph.HasValue())
            {
                builder.AppendFormat("<td><h4><i class='glyphicon glyphicon-{0}'></i></h4></td>", glyph);
            }
            else if (fontIcon.HasValue())
            {
                builder.AppendFormat("<td><h4><i class='fa fa-{0}'></i><h4></td>", fontIcon);
            }
            builder.AppendFormat("<td><h4><span id='{0}'>{1}</span></h4></td>", data.DisplayName, data.Model);
            builder.Append("</tr>");

            return new MvcHtmlString(builder.ToString());
        }
    }
}
