using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Mvc.Validators
{
    public class TimeAsStringAttribute : RegularExpressionAttribute
    {
        public TimeAsStringAttribute()
            : base("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$")
        { }
    }
}