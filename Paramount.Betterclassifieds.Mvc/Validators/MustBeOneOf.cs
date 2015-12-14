using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Paramount.Betterclassifieds.Mvc.Validators
{
    /// <summary>
    /// Validates that at least one of strings matches exactly. Useful for string and enumeration matching
    /// </summary>
    public class MustBeOneOf : ValidationAttribute
    {
        private readonly string[] _validItems;

        public MustBeOneOf(params string[] validItems)
        {
            _validItems = validItems;
        }

        public StringComparison Comparison { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var isValid = _validItems.Any(item => item.Equals(value.ToString(), Comparison));

            if (isValid)
                return ValidationResult.Success;

            return new ValidationResult(string.Format("{0} cannot be a value for {1}", value, validationContext.DisplayName), new[] { validationContext.MemberName });
        }
    }
}