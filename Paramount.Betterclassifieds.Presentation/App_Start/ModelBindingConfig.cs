using System;
using System.Web.Mvc;
using Paramount.ApplicationBlock.Mvc.ModelBinders;

namespace Paramount.Betterclassifieds.Presentation
{
    public class ModelBindingConfig
    {
        public static void Register(ModelBinderDictionary modelBinders)
        {
            modelBinders.Add(typeof(DateTime?), new ModelSpecificDateModelBinder());
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(RequiredIfAttribute), typeof(RequiredAttributeAdapter));
        }
    }
}