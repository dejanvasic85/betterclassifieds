using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Paramount
{
    public static class DropDownHtmlExtensions
    {
        // [Obsolete("Currently this doesn't really work because the selected value is not persisting when calling DropDownListFor.")]
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            string selectedValue = "", IDictionary<string, object> attributes = null)
        {
            ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            IEnumerable<SelectListItem> items = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString().ToCamelCaseWithSpaces(),
                    Value = e.ToString().ToEnumValue<TEnum>().ToString(CultureInfo.InvariantCulture)
                });

            var dropDown = helper.DropDownListFor(expression, items, attributes);

            return dropDown;
        }
    }
}