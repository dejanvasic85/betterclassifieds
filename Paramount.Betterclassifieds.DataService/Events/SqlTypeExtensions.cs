using System;

namespace Paramount.Betterclassifieds.DataService.Events
{
    internal static class SqlTypeExtensions
    {
        public static object SqlNullIfEmpty(this int? target)
        {
            if (target == null)
                return DBNull.Value;
            return target;
        }

        public static object SqlNullIfEmpty(this DateTime? target)
        {
            if (target == null)
                return DBNull.Value;
            return target;
        }

        public static object SqlNullIfEmpty(this string target)
        {
            if (target.IsNullOrEmpty())
                return DBNull.Value;
            return target;
        }
        public static object SqlNullIfEmpty(this bool? target)
        {
            if (target == null)
                return DBNull.Value;
            return target;
        }
    }
}