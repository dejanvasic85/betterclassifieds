using System;
using System.Web.UI;

namespace BetterClassified
{
    public static class ControlExtensions
    {
        public static T FindControl<T>(this Control target, string controlId, bool isRequired = true) where T : Control
        {
            var control = target.FindControl(controlId) as T;
            if (control == null && isRequired)
            {
                throw new NullReferenceException("Control cannot be found within target control. Please ensure grid is setup correctly.");
            }
            return control;
        }
    }
}