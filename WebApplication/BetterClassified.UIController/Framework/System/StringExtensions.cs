using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified
{
    public static class StringExtensions
    {
        public static int? ToInt(this string value)
        {
            int convertedValue;
            if (int.TryParse(value, out convertedValue))
                return convertedValue;
            return null;
        }
    }
}
