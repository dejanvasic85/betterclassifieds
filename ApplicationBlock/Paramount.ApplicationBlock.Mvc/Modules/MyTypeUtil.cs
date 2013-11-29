using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Paramount.ApplicationBlock.Mvc
{
    public static class MyTypeUtil
    {
        public static bool TypeIsPublicClass(Type type)
        {
            if (type != (Type)null && type.IsPublic && type.IsClass)
                return !type.IsAbstract;
            else
                return false;
        }

        public static IEnumerable<Type> GetFilteredTypesFromAssemblies(string @namespace, Predicate<Type> predicate)
        {
            var types = Assembly.Load(@namespace).GetTypes();

            return types.Where(type => MyTypeUtil.TypeIsPublicClass(type) && predicate(type));
        }

        public static IEnumerable<Type> GetFilteredTypesFromAssemblies(IEnumerable<string> namespaces, Predicate<Type> predicate)
        {            
            var enumerable = new List<Type>();

            foreach (var item in namespaces.Select(a => GetFilteredTypesFromAssemblies(a, predicate)))
            {
                enumerable.AddRange(item);
            }

            return enumerable;
        }
    }
}