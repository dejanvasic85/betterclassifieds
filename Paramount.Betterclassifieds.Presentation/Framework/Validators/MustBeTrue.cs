using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds
{
    /// <summary>
    /// Used mostly for agreeing to terms and conditions and verification checkboxes
    /// </summary>
    public class MustBeTrue : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool) value;
        }
    }
}
