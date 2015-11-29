using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Mvc.Validators
{
    public class MaxWordsAttribute : ValidationAttribute
    {
        private string LinkedPropertyName { get; set; }

        public MaxWordsAttribute(string linkedPropertyName)
        {
            this.LinkedPropertyName = linkedPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var propertyValue = validationContext.ObjectType
                .GetProperty(LinkedPropertyName)
                .GetValue(validationContext.ObjectInstance, null);

            if (value.ToString().WordCount() > (int)propertyValue)
            {
                return new ValidationResult(string.Format("Word count of {0} has been exceeded.", propertyValue));
            }

            return ValidationResult.Success;
        }
    }
}