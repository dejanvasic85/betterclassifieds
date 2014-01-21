using System.Collections.Generic;

namespace Paramount
{
    using System;

    public static class ArrayExtensions
    {
        public static TResult[] CastAll<TSource, TResult>(this TSource[] source) where TResult : class
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            TResult[] result = new TResult[source.Length];

            for (int index = 0; index < source.Length; index++)
            {
                result[index] = source[index] as TResult;
            }

            return result;
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> data)
        {
            return data ?? new List<T>();
        }
    }
}