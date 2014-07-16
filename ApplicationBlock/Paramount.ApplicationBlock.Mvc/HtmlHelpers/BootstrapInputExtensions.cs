using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Paramount
{
    public static class BootstrapInputExtensions
    {
        /// <summary>
        /// Generates an input element only with a form-control css class
        /// </summary>
        public static MvcHtmlString BootstrapTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            return htmlHelper.TextBoxFor(expression, format: null, htmlAttributes: AddFormControlClass(attributes));
        }

        /// <summary>
        /// Simply replaces the type=text with type=email for html5 purposes and adds form-control css class
        /// </summary>
        public static MvcHtmlString BootstrapEmailFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            var bootstrapTextBox = htmlHelper.BootstrapTextBoxFor(expression).ToHtmlString();

            var email = bootstrapTextBox.Replace("type=\"text\"", "type=\"email\"");

            return new MvcHtmlString(email);
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
