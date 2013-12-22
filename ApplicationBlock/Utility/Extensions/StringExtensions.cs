namespace Paramount
{
    using System;

    public static class StringExtensions
    {
        public static bool DoesNotEqual(this string source, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return !source.Equals(value, comparison);
        }

        public static bool EqualTo(this string source, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {   
            return source.Equals(value, comparison);
        }

        public static string Default(this string source, string value)
        {
            if (source.IsNullOrEmpty())
                return value;
            return source;
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool HasValue(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        public static string Append(this string source, string value)
        {
            return string.Format("{0}{1}", source, value);
        }

        public static string Prefix(this string source, string prefixValue)
        {
            return Append(prefixValue, source);
        }

        public static int? ToInt(this string value)
        {
            int convertedValue;
            if (int.TryParse(value, out convertedValue))
                return convertedValue;
            return null;
        }
    }
}
