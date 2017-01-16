using System;
using System.Globalization;
using System.Web.Mvc;

namespace Paramount.Betterclassifieds.Presentation.Framework.ModelBinders
{
    public class ModelSpecificDateModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                DateTime date;
                displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                // use the format specified in the DisplayFormat attribute to parse the date
                if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    if (bindingContext.ModelMetadata.IsRequired)
                    {
                        bindingContext.ModelState.AddModelError(
                            bindingContext.ModelName,
                            $"{value.AttemptedValue} is an invalid date format");
                    }
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
