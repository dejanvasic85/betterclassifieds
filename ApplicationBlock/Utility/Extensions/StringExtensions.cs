using System.Text;

namespace Paramount
{
    using System;

    public static class StringExtensions
    {
        private const string ellipsis = "...";

        public static string Truncate(this string content, int maxLength)
        {
            return content.Truncate(maxLength, ellipsis);
        }

        public static string NullIfEmpty(this string content)
        {
            return string.IsNullOrWhiteSpace(content) ? null : content;
        }

        public static string Truncate(this string content, int maxLength, string suffix)
        {
            if (maxLength < 1)
                throw new ArgumentOutOfRangeException("maxLength", maxLength, "MaxLength must be positive");

            if (content.IsNullOrEmpty())
                return String.Empty;

            return (content.Length <= maxLength)
                ? content
                : content.Substring(0, maxLength).TrimEnd() + suffix;
        }

        public static string TruncateOnWordBoundary(this string content, int maxLength)
        {
            return content.TruncateOnWordBoundary(maxLength, ellipsis);
        }

        public static string TruncateOnWordBoundary(this string content, int maxLength, string suffix)
        {
            if (content.IsNullOrEmpty())
                return String.Empty;

            if (content.Length < maxLength)
                return content;

            int i = maxLength;
            while (i > 0)
            {
                if (Char.IsWhiteSpace(content[i]))
                    break;
                i--;
            }

            return content.Truncate(i, suffix);
        }

        public static string EmptyAsNull(this string content)
        {
            return content.IsNullOrEmpty() ? null : content;
        }

        public static bool DoesNotEqual(this string source, string value,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return !source.Equals(value, comparison);
        }

        public static bool EqualTo(this string source, string value,
            StringComparison comparison = StringComparison.OrdinalIgnoreCase)
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

        public static int ToEnumValue<T>(this string value)
        {
            return (int)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// Adds spaces to camel cased words e.g. NewestFirst becomes "Newest First"
        /// </summary>
        ///<remarks>
        /// See http://stackoverflow.com/questions/272633/add-spaces-before-capital-letters for more info.
        /// </remarks>
        public static string ToCamelCaseWithSpaces(this string text, bool preserveAcronyms = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    {
                        newText.Append(' ');
                    }
                }
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
