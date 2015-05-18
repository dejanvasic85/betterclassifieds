namespace Paramount.Betterclassifieds.Tests.Membership
{
    using Business;
    using Business.Broadcast;
    using Mocks;
    using Moq;
    using NUnit.Framework;
    using Paramount.Utility;

    [TestFixture]
    public class UserManagerTests :  TestContext<UserManager>
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAuthManager> _mockAuthManager;
        private Mock<IBroadcastManager> _mockBroadcastManager;
        private Mock<IClientConfig> _mockConfig;
        private Mock<IConfirmationCodeGenerator> _mockCodeGenerator;
        private Mock<IDateService> _mockDateService;

        [SetUp]
        public void SetupMocks()
        {
            _mockUserRepository = CreateMockOf<IUserRepository>();
            _mockAuthManager = CreateMockOf<IAuthManager>();
            _mockBroadcastManager = CreateMockOf<IBroadcastManager>();
            _mockConfig = CreateMockOf<IClientConfig>();
            _mockCodeGenerator = CreateMockOf<IConfirmationCodeGenerator>();
            _mockDateService = CreateMockOf<IDateService>();
        }
        
        [Test]
        public void ConfirmRegistration_RegistrationIsNull_Returns_RegistrationDoesNotExist()
        {
            // Arrange 
            _mockAuthManager.SetupWithVerification(call => call.GetRegistration(It.IsAny<int>()), null);
            
            // Act 
            var confirmResult = BuildTargetObject().ConfirmRegistration(100, string.Empty);

            // Assert
            confirmResult.IsEqualTo(RegistrationConfirmationResult.RegistrationDoesNotExist);
        }
    }
}