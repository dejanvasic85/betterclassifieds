﻿using System;
using System.Collections.Generic;

namespace Paramount
{
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
    }
}