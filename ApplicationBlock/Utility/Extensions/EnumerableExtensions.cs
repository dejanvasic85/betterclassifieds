using System.Linq;

namespace Paramount
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            if (action == null)
                throw new ArgumentNullException("action");

            foreach (T item in collection)
            {
                action(item);
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> data)
        {
            return data ?? new List<T>();
        }

        public static IEnumerable<T> NullIfEmpty<T>(this IEnumerable<T> data)
        {
            return data ?? null;
        }

    }
}