using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Security;

namespace Paramount.Betterclassifieds.Tests.Membership
{
    [TestFixture]
    public class ConfirmationCodeGeneratorTests
    {
        [Test]
        public void Generate_Returns_FourDigits()
        {
            IConfirmationCodeGenerator generator = new ConfirmationCodeGenerator();

            var result = generator.GenerateCode();

            Assert.That(result.Length, Is.EqualTo(4));
        }
    }
}
