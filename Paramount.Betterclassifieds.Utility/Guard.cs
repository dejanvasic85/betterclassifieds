using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq.Expressions;

namespace Paramount
{
    [DebuggerStepThrough]
    public static class Guard
    {
        /// <summary>
        /// Ensures the given <paramref name="value"/> is not null.
        ///             Throws <see cref="T:System.ArgumentNullException"/> otherwise.
        /// 
        /// </summary>
        public static void NotNull<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException(typeof(T).Name);
        }

        public static void NotNullIn(params object[] values)
        {
            foreach (var value in values)
            {
                NotNull(value);
            }   
        }

        /// <summary>
        /// Ensures the given string <paramref name="value"/> is not null or empty.
        ///             Throws <see cref="T:System.ArgumentNullException"/> in the first case, or
        ///             <see cref="T:System.ArgumentException"/> in the latter.
        /// 
        /// </summary>
        public static void NotNullOrEmpty(string value)
        {
            NotNull(value);
            if (value.Length == 0)
                throw new ArgumentException("Argument cannot be empty", "value");
        }

        public static void NotNullOrEmpty(string[] values)
        {
            NotNull(values);
            if (values.Length == 0)
                throw new ArgumentException("Argument cannot be empty", "values");
        }

        public static void NotDefaultValue<T>(T value)
        {
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
                throw new ArgumentException("Argument cannot be default value");
        }
        
        /// <summary>
        /// Checks an argument to ensure it is in the specified range including the edges.
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the argument to check, it must be an <see cref="T:System.IComparable"/> type.
        ///             </typeparam><param name="reference">The expression containing the name of the argument.</param><param name="value">The argument value to check.</param><param name="from">The minimun allowed value for the argument.</param><param name="to">The maximun allowed value for the argument.</param>
        public static void NotOutOfRangeInclusive<T>(Expression<Func<T>> reference, T value, T from, T to) where T : IComparable
        {
            if (value != null && (value.CompareTo(@from) < 0 || value.CompareTo(to) > 0))
                throw new ArgumentOutOfRangeException(GetParameterName(reference));
        }

        /// <summary>
        /// Checks an argument to ensure it is in the specified range excluding the edges.
        /// 
        /// </summary>
        /// <typeparam name="T">Type of the argument to check, it must be an <see cref="T:System.IComparable"/> type.
        ///             </typeparam><param name="reference">The expression containing the name of the argument.</param><param name="value">The argument value to check.</param><param name="from">The minimun allowed value for the argument.</param><param name="to">The maximun allowed value for the argument.</param>
        public static void NotOutOfRangeExclusive<T>(Expression<Func<T>> reference, T value, T from, T to) where T : IComparable
        {
            if (value != null && (value.CompareTo(@from) <= 0 || value.CompareTo(to) >= 0))
                throw new ArgumentOutOfRangeException(GetParameterName(reference));
        }

        public static void CanBeAssigned(Expression<Func<object>> reference, Type typeToAssign, Type targetType)
        {
            if (targetType.IsAssignableFrom(typeToAssign))
                return;
            if (targetType.IsInterface)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type does not implement an interface", new object[2]
        {
          typeToAssign,
          targetType
        }), GetParameterName(reference));
            throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "The type does not implement an interface", new object[2]
            {
                typeToAssign,
                targetType
            }), GetParameterName(reference));
        }

        private static string GetParameterName(LambdaExpression reference)
        {
            return ((MemberExpression)reference.Body).Member.Name;
        }
    }
}
