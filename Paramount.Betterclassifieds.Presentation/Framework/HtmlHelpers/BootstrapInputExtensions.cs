﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
            if (attributes == null)
                attributes = new Dictionary<string, object>();

            return htmlHelper.TextBoxFor(expression, format: null, htmlAttributes: attributes.WithFormControl());
        }

        /// <summary>
        /// Generates an input element only with a form-control css class and large input
        /// </summary>
        public static MvcHtmlString BootstrapLargeTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null, string format = "")
        {
            if (attributes == null)
                attributes = new Dictionary<string, object>();

            return htmlHelper.TextBoxFor(expression,
                format: format,
                htmlAttributes: attributes.WithLargeFormControl());
        }

        /// <summary>
        /// Generates an textarea element only with a form-control css class and large input
        /// </summary>
        public static MvcHtmlString BootstrapLargeTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, int rows, IDictionary<string, object> attributes = null)
        {
            if (attributes == null)
                attributes = new Dictionary<string, object>();

            return htmlHelper.TextAreaFor(expression,
                htmlAttributes: attributes.WithLargeFormControl().WithRows(rows));
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

        /// <summary>
        /// Simply replaces the type=text with type=number for html5 purposes and adds form-control css class
        /// </summary>
        public static MvcHtmlString BootstrapNumberFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            var bootstrapTextBox = htmlHelper.BootstrapTextBoxFor(expression, attributes).ToHtmlString();

            var numberInput = bootstrapTextBox.Replace("type=\"text\"", "type=\"number\"");

            return new MvcHtmlString(numberInput);
        }

        /// <summary>
        /// Simply replaces the type=text with type=number for html5 purposes and adds form-control css class
        /// </summary>
        public static MvcHtmlString BootstrapTelFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            var bootstrapTextBox = htmlHelper.BootstrapTextBoxFor(expression).ToHtmlString();

            var numberInput = bootstrapTextBox.Replace("type=\"text\"", "type=\"tel\"");

            return new MvcHtmlString(numberInput);
        }

        /// <summary>
        /// Generates an input element with calendar class and data-provide attributes
        /// </summary>
        public static MvcHtmlString BootstrapCalendar<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> attributes = null)
        {
            if (attributes == null)
                attributes = new Dictionary<string, object>();
            attributes.WithLargeFormControl().WithCalendar();
            return htmlHelper.BootstrapLargeTextBoxFor(expression, attributes, "{0:dd/MM/yyyy}");
        }

    }
}
