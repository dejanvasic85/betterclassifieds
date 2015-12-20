using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests
{
    public static class AssertExtensions
    {
        #region IsNotNull
        public static T IsNotNull<T>(this T target, string message = null, params object[] parameters)
        {
            Assert.IsNotNull(target, message, parameters);
            
            return target;
        }
        #endregion

        #region IsNull
        public static void IsNull<T>(this T target, string message = null, params object[] parameters)
        {
            Assert.IsNull(target, message, parameters);
        }
        #endregion

        #region AreEqual
        public static string AreEqual(this string target, string expected, bool ignoreCase, string message = null, params object[] parameters)
        {
            Assert.AreEqual(expected, target);
            return target;
        }

        public static T AreEqual<T>(this T target, T expected, string message = null, params object[] parameters)
        {
            Assert.AreEqual(expected, target, message, parameters);
            return target;
        }

        #endregion

        #region IsEqualTo
        public static string IsEqualTo(this string target, string expected, bool ignoreCase, string message = null, params object[] parameters)
        {
            Assert.AreEqual(expected, target);
            return target;
        }

        public static T IsEqualTo<T>(this T target, T expected, string message = null, params object[] parameters)
        {
            Assert.That(target, Is.EqualTo(expected), message, parameters);
            return target;
        }
        #endregion

        #region IsNotEqualTo
        public static string IsNotEqualTo(this string target, string expected, bool ignoreCase, string message = null, params object[] parameters)
        {
            Assert.AreNotEqual(expected, target);
            return target;
        }

        public static T IsNotEqualTo<T>(this T target, T expected, string message = null, params object[] parameters)
        {
            Assert.AreNotEqual(expected, target, message, parameters);
            return target;
        }
        #endregion

        #region IsTrue
        public static void IsTrue(this bool target, string message = null, params object[] parameters)
        {
            Assert.IsTrue(target, message, parameters);
        }

        public static void IsTrue(this bool? target, string message = null, params object[] parameters)
        {
            target.IsNotNull();
            Assert.IsTrue(target.Value, message, parameters);
        }
        #endregion

        #region IsFalse
        public static void IsFalse(this bool target, string message = null, params object[] parameters)
        {
            Assert.IsFalse(target, message, parameters);
        }

        public static void IsFalse(this bool? target, string message = null, params object[] parameters)
        {
            target.IsNotNull();
            Assert.IsFalse(target.Value, message, parameters);
        }
        #endregion

        public static void IsLargerThan(this int target, int compareTo)
        {
            Assert.IsTrue(target > compareTo);
        }

        public static void IsLargerThan(this long target, long compareTo)
        {
            Assert.IsTrue(target > compareTo);
        }

        public static void IsSameAs<T>(this T target, T toCompare)
        {
            Assert.AreSame(toCompare, target);
        }

        public static void IsDifferentTo<T>(this T target, T toCompare)
        {
            Assert.AreNotSame(toCompare, target);
        }

        public static void IsTypeOf<T>(this object target, string message = null, params object[] parameters)
        {
            Assert.IsInstanceOf<T>(target);
        }

        public static void AreAllTrue<T>(this IEnumerable<T> items, Predicate<T> predicate)
        {
            foreach (var item in items)
            {
                IsTrue(predicate(item));
            }
        }

        public static void IsRedirectingTo(this ActionResult actionResult, string expected)
        {
            var redirectResult = (RedirectResult) actionResult;
            Assert.That(redirectResult.Url, Is.EqualTo(expected));
        }

        public static void JsonResultContains(this ActionResult actionResult, string expected)
        {
            var jsonResult = (JsonResult) actionResult;
            Assert.That(jsonResult.Data.ToString(), Is.EqualTo(expected));
        }

        public static TExpected ViewResultModelIsTypeOf<TExpected>(this ActionResult actionResult)
        {
            var viewResult = (ViewResult) actionResult;
            var model = viewResult.Model;
            Assert.That(model, Is.TypeOf<TExpected>());
            return (TExpected)model;
        }
    }
}
