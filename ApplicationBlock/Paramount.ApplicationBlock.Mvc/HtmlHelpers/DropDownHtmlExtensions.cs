using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Paramount.ApplicationBlock.Mvc.HtmlHelpers
{
    public static class DropDownHtmlExtensions
    {
        public static MvcHtmlString DropDownListForEnum<TModel, TProperty, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, 
            TEnum selectedValue, IDictionary<string, object> attributes)
        {

            IEnumerable<SelectListItem> items = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new SelectListItem
                {
                    Text = e.ToString().ToCamelCaseWithSpaces(),
                    Value = e.ToString().ToEnumValue<TEnum>().ToString(),
                    Selected = e.Equals(selectedValue)
                });


            return helper.DropDownListFor(expression, items);
        }
    }
}