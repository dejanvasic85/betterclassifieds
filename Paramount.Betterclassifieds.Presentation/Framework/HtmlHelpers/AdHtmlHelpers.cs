using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace Paramount
{
    public static class AdHtmlHelpers
    {
        /// <summary>
        /// Renders a table row in the contact and ad details sections when viewing an ad
        /// </summary>
        public static MvcHtmlString AdContactDetail<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string glyph = "", string fontIcon = "")
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (data.Model == null || data.Model.ToString().IsNullOrEmpty())
                return MvcHtmlString.Empty;

            var content = string.Format("<td><h4><span id='{0}'>{1}</span></h4></td>", data.PropertyName, data.Model);
            return CreateRowElementTemplate(content, glyph, fontIcon);
        }

        public static MvcHtmlString AdContactDetailPhone<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string glyph = "", string fontIcon = "")
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (data.Model == null || data.Model.ToString().IsNullOrEmpty())
                return MvcHtmlString.Empty;

            var content = string.Format("<td><h4><a href='tel:+{1}'><span id='{0}'>{1}</span></a></h4></td>", data.PropertyName, data.Model);

            return CreateRowElementTemplate(content, glyph, fontIcon);
        }

        public static MvcHtmlString AdContactDetailEmail<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string subject = "", string glyph = "", string fontIcon = "")
        {
            var data = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            if (data.Model == null || data.Model.ToString().IsNullOrEmpty())
                return MvcHtmlString.Empty;

            var content = string.Format("<td><h4><a href='mailto:{1}?subject={2}'><span id='{0}'>{1}</span></a></h4></td>", data.PropertyName, data.Model, subject);

            return CreateRowElementTemplate(content, glyph, fontIcon);
        }

        public static MvcHtmlString CreateRowElementTemplate(string content, string glyph = "", string fontIcon = "")
        {
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
            builder.Append(content);
            builder.Append("</tr>");

            return new MvcHtmlString(builder.ToString());
        }
    }
}
