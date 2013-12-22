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
    }
}