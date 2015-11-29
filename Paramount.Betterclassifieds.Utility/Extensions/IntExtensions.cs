namespace Paramount
{
    public static class IntExtensions
    {
        /// <summary>
        /// Returns true if the <param name="value"></param> is null. Reads better :)
        /// </summary>
        public static bool IsNullValue(this int? value)
        {
            return !value.HasValue;
        }
    }
}