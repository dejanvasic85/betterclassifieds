using System;
using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Mvc.Validators
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private object DesiredValue { get; set; }
        public string ValidationMessage { get; set; }

        public RequiredIfAttribute(string propertyName, object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var instance = context.ObjectInstance;
            var type = instance.GetType();
            var targetSiblingValue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (value == null && targetSiblingValue.ToString() == DesiredValue.ToString())
            {
                return new ValidationResult(ValidationMessage, new[] { context.MemberName });
            }
            return ValidationResult.Success;
        }
    }
}