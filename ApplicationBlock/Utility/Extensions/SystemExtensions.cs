using System.IO;

namespace Paramount
{
    public static class SystemExtensions
    {
        public static TTo CastTo<TTo>(this object target)
        {
            return target is TTo ? (TTo) target : default(TTo);
        }
    }
}