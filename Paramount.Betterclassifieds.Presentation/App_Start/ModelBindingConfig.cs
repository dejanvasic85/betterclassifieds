using System;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Presentation.Framework.ModelBinders;

namespace Paramount.Betterclassifieds.Presentation
{
    public class ModelBindingConfig
    {
        public static void Register(ModelBinderDictionary modelBinders)
        {
            // Custom model binders
            modelBinders.Add(typeof(DateTime?), new ModelSpecificDateModelBinder());
            modelBinders.Add(typeof(BookingCart), new BookingCartBinder());

            // Attributes
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredAttributeAdapter));

        }
    }
}