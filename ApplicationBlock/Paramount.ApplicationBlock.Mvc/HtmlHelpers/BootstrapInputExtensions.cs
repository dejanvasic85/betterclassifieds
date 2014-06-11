using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Paramount
{
    public static class BootstrapInputExtensions
    {
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            return htmlHelper.TextBoxFor(expression, format: null, htmlAttributes: AddFormControlClass(attributes));
        }
        
        private static IDictionary<string, object> AddFormControlClass(IDictionary<string, object> attributes)
        {
            if (attributes == null)
                attributes = new Dictionary<string, object>();

            attributes.Add("class", "form-control");
            return attributes;
        }

    }

}
