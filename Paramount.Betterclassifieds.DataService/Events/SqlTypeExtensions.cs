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
    }
}