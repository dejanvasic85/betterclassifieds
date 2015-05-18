using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Security;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.Tests.Membership
{
    [TestFixture]
    public class ConfirmationCodeGeneratorTests
    {
        [Test]
        public void Generate_Returns_FourDigits()
        {
            var dateService = new Mock<IDateService>();
            dateService.Setup(call => call.Now).Returns(DateTime.Now);
            dateService.Setup(call => call.UtcNow).Returns(DateTime.UtcNow);

            IConfirmationCodeGenerator generator = new ConfirmationCodeGenerator(dateService.Object);

            var result = generator.GenerateCode();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ConfirmationCode.Length, Is.EqualTo(ConfirmationCodeGenerator.CodeLength));
        }
    }
}
