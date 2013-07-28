﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        public static IEnumerable<T> FindControls<T>(this Control target) where T : Control
        {
            return target.Controls
                .Cast<Control>()
                .Where(control => control.GetType() == typeof (T))
                .Select(control => (T) control);
        }

        public static void RemoveCssClass(this WebControl target, string @class)
        {
            target.CssClass = String.Join(" ", target.CssClass.Split(' ').Where(x => x != @class).ToArray());
        }
    }
}