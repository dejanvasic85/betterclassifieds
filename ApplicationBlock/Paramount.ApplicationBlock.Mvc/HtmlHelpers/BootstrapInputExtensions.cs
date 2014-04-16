using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Paramount.ApplicationBlock.Mvc.HtmlHelpers
{
    public static class BootstrapInputExtensions
    {
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            if(attributes == null)
                attributes = new Dictionary<string, object>();

            attributes.Add("class", "form-control");
            return htmlHelper.TextBoxFor(expression, format: null, htmlAttributes: attributes);
        }
    }
}
