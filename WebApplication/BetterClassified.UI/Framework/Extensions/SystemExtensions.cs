using System;
using System.Reflection;

namespace BetterClassified
{
    public static class SystemExtensions
    {
        public static T[] GetCustomAttributes<T>(this MemberInfo memberInfo) where T : Attribute
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            object[] customAttrs = memberInfo.GetCustomAttributes(typeof(T), true);

            return customAttrs.CastAll<object, T>();
        }

        public static T GetCustomAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            T[] customAttrs = memberInfo.GetCustomAttributes<T>();

            if (customAttrs == null || customAttrs.Length == 0)
                return null;

            if (customAttrs.Length > 1)
                throw new ArgumentException("More than 1 attribute found of type " + typeof(T).GetType() + " on member " + memberInfo.Name);

            return customAttrs[0];
        }

        public static bool HasCustomAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            return memberInfo.GetCustomAttributes<T>().Length > 0;
        }
    }
}