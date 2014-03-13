using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Paramount.Betterclassifieds.Tests
{
    /// <summary>
    /// Helper class for when you expect an exception in unit tests.
    /// </summary>
    public static class Expect
    {
        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type</typeparam>
        /// <param name="action">The action to execute</param>
        public static void Exception<T>(Action action) where T : Exception
        {
            Exception<T>(action, null, null);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="predicate">The predicate.</param>
        public static void Exception<T>(Action action, Func<T, bool> predicate ) where T : Exception
        {
            Exception<T>(action, predicate, null);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="expectedMessage">The expected message.</param>
        public static void Exception<T>(Action action, string expectedMessage) where T : Exception
        {
            Exception<T>(action, null, expectedMessage);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="expectedMessage">The expected message.</param>
        public static void Exception<T>(Action action, Func<T, bool> predicate, string expectedMessage) where T : Exception
        {
            ExecuteAndAnalyse(action, predicate, expectedMessage, false);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type somewhere in the exception hierarchy</typeparam>
        /// <param name="action">The action.</param>
        public static void ExceptionInChain<T>(Action action) where T : Exception
        {
            ExceptionInChain<T>(action, null, null);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type somewhere in the exception hierarchy</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="expectedMessage">The expected message.</param>
        public static void ExceptionInChain<T>(Action action, string expectedMessage) where T : Exception
        {
            ExceptionInChain<T>(action, null, expectedMessage);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type somewhere in the exception hierarchy</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="predicate">The predicate.</param>
        public static void ExceptionInChain<T>(Action action, Func<T, bool> predicate) where T : Exception
        {
            ExceptionInChain<T>(action, predicate, null);
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type somewhere in the exception hierarchy</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="expectedMessage">The expected message.</param>
        public static void ExceptionInChain<T>(Action action, Func<T, bool> predicate, string expectedMessage) where T : Exception
        {
            ExecuteAndAnalyse( action, predicate, expectedMessage, true );
        }

        /// <summary>
        /// Executes an action, expecting an exception to be raised.
        /// </summary>
        /// <typeparam name="T">The expected exception type somewhere in the exception hierarchy</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="expectedMessage">The expected message.</param>
        private static void ExecuteAndAnalyse<T>(Action action, Func<T, bool> predicate, string expectedMessage, bool walkInnerExceptions ) where T : Exception
        {
            try
            {
                action();
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Exception current = ex;

                while (walkInnerExceptions && current != null && typeof(T) != current.GetType())
                    current = current.InnerException;

                ex = current ?? ex;

                Assert.AreEqual(typeof(T), ex.GetType(), "Exception of type {0} was thrown instead of {1}\n{2}", ex.GetType().ToString(), typeof(T).ToString(), ex.ToString());

                if (predicate != null)
                    Assert.IsTrue(predicate((T)ex), "Exception predicate failed - " + ex.ToString());

                if (expectedMessage != null)
                    Assert.AreEqual(expectedMessage, ex.Message, "Expected exception to have message: \"{0}\" but received message \"{1}\"", expectedMessage, ex.Message);

                return;
            }

            Assert.Fail("Exception of type {0} was not thrown", typeof(T).ToString());
        }
    }
}
