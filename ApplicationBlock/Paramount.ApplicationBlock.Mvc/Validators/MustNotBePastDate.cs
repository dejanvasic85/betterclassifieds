using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Paramount.Utility;

namespace Paramount.ApplicationBlock.Mvc.Validators
{
    public class MustNotBePastDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            var valueDateTime = Convert.ToDateTime(value);

            var dateService = DependencyResolver.Current.GetService<IDateService>();

            if (valueDateTime < dateService.Today)
            {
                return false;
            }

            return true;
        }
    }
}