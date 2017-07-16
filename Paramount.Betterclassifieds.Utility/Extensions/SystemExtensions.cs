using System;
using System.IO;

namespace Paramount
{
    public static class SystemExtensions
    {
        public static TTo As<TTo>(this object target)
        {
            return target is TTo ? (TTo) target : default(TTo);
        }

        public static byte[] ToBytes(this Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string ToIsoDateString(this DateTime dateTime)
        {
            return $"{dateTime:s}";
        }

        public static string ToIsoDateString(this DateTime? dateTime)
        {
            return dateTime?.ToIsoDateString();
        }


        public static string ToUtcIsoDateString(this DateTime dateTime)
        {
            return string.Concat(dateTime.ToString("s"), "Z");
        }

        public static string ToUtcIsoDateString(this DateTime? dateTime)
        {
            return dateTime?.ToUtcIsoDateString();
        }
    }
}