using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paramount.ApplicationBlock.Mvc.Validators
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
