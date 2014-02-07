using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public static string[] EmptyIfNull(this string[] data)
        {
            return data ?? new List<string>().ToArray();
        }

        public static int[] EmptyIfNull(this int[] data)
        {
            return data ?? new List<int>().ToArray();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> data)
        {
            return data == null || !data.Any();
        }

        public static bool IsNullOrEmpty(this Array data)
        {
            return data == null || data.Length == 0 ;
        }
    }
}