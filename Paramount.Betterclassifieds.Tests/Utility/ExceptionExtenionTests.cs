using System;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    public class ExceptionExtenionTests
    {
        [Test]
        public void InnerMostException_HasTwoInnerExceptions_ReturnsMostInner()
        {
            // arrange
            // act
            try
            {
                DieHard("this won't die... but func 2 will");
            }
            catch (Exception ex)
            {
                // assert
                Assert.That(ex, Is.TypeOf<DieException>());
                Assert.That(ex.InnerMostException(), Is.TypeOf<InvalidOperationException>(), "Boom die hard two");
            }
        }

        [Test]
        public void InnerMostException_HasOneException_ReturnsFirst()
        {
            // arrange
            // act
            try
            {
                DieHard(null);
            }
            catch (Exception ex)
            {
                // assert
                Assert.That(ex.InnerMostException, Is.TypeOf<ArgumentException>(), "Boom die hard");
            }
        }

        private void DieHard(string badArg)
        {
            if (string.IsNullOrEmpty(badArg))
            {
                throw new ArgumentException("Boom die hard");
            }

            try
            {
                DieHardTwo();
            }
            catch (Exception e)
            {
                throw new DieException(e);
            }
        }

        private void DieHardTwo()
        {
            throw new InvalidOperationException("Boom die hard two");
        }

        private class DieException : Exception
        {
            public DieException(Exception innerException)
                : base("Failed to die", innerException)
            {
                
            }
        }
    }
}