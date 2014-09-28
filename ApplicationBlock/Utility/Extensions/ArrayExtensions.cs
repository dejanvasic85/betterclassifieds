using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public static byte[] FromStream(this Stream stream)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    } 
}