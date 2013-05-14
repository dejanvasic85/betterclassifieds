using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BetterClassified
{
    public static class StringExtensions
    {
        public static bool DoesNotEqual(this string source, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return !source.Equals(value, comparison);
        }
    }
}
