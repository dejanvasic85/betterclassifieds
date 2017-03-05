using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Presentation.Services;

namespace Paramount.Betterclassifieds.Tests.PresentationServices
{
    [TestFixture]
    public class MailServiceTests : TestContext<MailService>
    {
        private Mock<ITemplatingService> _templatingService;
        private Mock<IUrl> _urlMock;

        [Test]
        public void SendEventOrganiserInvite()
        {

        }

    }
}
