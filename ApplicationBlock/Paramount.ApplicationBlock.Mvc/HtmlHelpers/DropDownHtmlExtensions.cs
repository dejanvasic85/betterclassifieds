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
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
            TEnum defaultValue, string name = "", string @class = "")
        {
            IEnumerable<SelectListItem> items = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString().ToCamelCaseWithSpaces(),
                    Value = e.ToString().ToEnumValue<TEnum>().ToString(CultureInfo.InvariantCulture)
                });

            IDictionary<string, object> attributes = new Dictionary<string, object>();
            if (name.HasValue())
            {
                attributes.Add("id", name);
                attributes.Add("Name", name); // Need capital to override the default! Crazy... I know
            }

            if (@class.HasValue())
            {
                attributes.Add("class", @class);
            }
            
            return helper.DropDownListFor(expression, items, attributes);
        }
    }
}