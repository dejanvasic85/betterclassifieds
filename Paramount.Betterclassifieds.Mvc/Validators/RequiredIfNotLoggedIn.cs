using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Paramount.Betterclassifieds.Mvc.Validators
{
    public class RequiredIfNotLoggedIn : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null || HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(string.Format("{0} is required if you are not logged in already.", validationContext.MemberName));
        }
    }
}