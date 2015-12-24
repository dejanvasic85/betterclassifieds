using Paramount.Betterclassifieds.Business.Payment;

namespace Paramount.Betterclassifieds.Tests.Membership
{
    using Business;
    using Business.Broadcast;
    using Mocks;
    using Moq;
    using NUnit.Framework;
    using Paramount.Utility;

    [TestFixture]
    public class UserManagerTests : TestContext<UserManager>
    {
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

        [Test]
        public void UpdateUserProfile_RetrievesOriginal_MapsProperties_UpdatesRepository()
        {
            // arrange
            var applicationUserMockBuilder = new ApplicationUserMockBuilder();
            var mockUser = applicationUserMockBuilder.Default().Build();
            var mockUserOriginal = applicationUserMockBuilder
                .WithUsername(mockUser.Username)
                .WithFirstName("original first")
                .WithLastName("original last")
                .WithAddressLine1("original add 1")
                .WithAddressLine2("original add 2")
                .WithPostcode("original postcode")
                .WithState("original State")
                .WithPreferredPaymentMethod(PaymentType.None)
                .WithPayPalEmail("original Paypal@email.com")
                .WithBankName("original bank")
                .WithBankAccountName("original account name")
                .WithBankAccountNumber("original account number")
                .WithBankBsbNumber("original account bsb")
                .Build()
                ;

            _mockUserRepository.SetupWithVerification(call => call.GetUserByEmail(It.Is<string>(p => p == mockUser.Username)), mockUserOriginal);
            _mockUserRepository.SetupWithVerification(call => call.UpdateUserProfile(It.Is<ApplicationUser>(p => p == mockUserOriginal)));

            // act
            BuildTargetObject().UpdateUserProfile(mockUser);

            // assert that all the relevant properties have been updated
            Assert.That(mockUserOriginal.FirstName, Is.EqualTo(mockUser.FirstName));
            Assert.That(mockUserOriginal.LastName, Is.EqualTo(mockUser.LastName));
            Assert.That(mockUserOriginal.AddressLine1, Is.EqualTo(mockUser.AddressLine1));
            Assert.That(mockUserOriginal.AddressLine2, Is.EqualTo(mockUser.AddressLine2));
            Assert.That(mockUserOriginal.Postcode, Is.EqualTo(mockUser.Postcode));
            Assert.That(mockUserOriginal.State, Is.EqualTo(mockUser.State));
            Assert.That(mockUserOriginal.PreferredPaymentMethod, Is.EqualTo(mockUser.PreferredPaymentMethod));
            Assert.That(mockUserOriginal.PayPalEmail, Is.EqualTo(mockUser.PayPalEmail));
            Assert.That(mockUserOriginal.BankName, Is.EqualTo(mockUser.BankName));
            Assert.That(mockUserOriginal.BankAccountName, Is.EqualTo(mockUser.BankAccountName));
            Assert.That(mockUserOriginal.BankAccountNumber, Is.EqualTo(mockUser.BankAccountNumber));
            Assert.That(mockUserOriginal.BankBsbNumber, Is.EqualTo(mockUser.BankBsbNumber));
        }

        [Test]
        public void LoginOrRegisterUser_UserExists_ValidatesPassword_ReturnsSuccess()
        {
            // arrange data
            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithEmail(mockApplicationUser.Email)
                .Build();
            var mockPassword = "password123";

            // arrange service calls
            _mockUserRepository.SetupWithVerification(call => call.GetUserByEmail(It.Is<string>(str => str == mockApplicationUser.Email)), mockApplicationUser);
            _mockAuthManager.SetupWithVerification(call => call.ValidatePassword(It.Is<string>(str => str == mockApplicationUser.Username), It.Is<string>(str => str == mockPassword)), true);
            _mockAuthManager.SetupWithVerification(call => call.Login(It.Is<string>(str => str == mockApplicationUser.Username), It.Is<bool>(p => p== false), It.IsAny<string>()));

            // act
            var userManager = BuildTargetObject();
            var result = userManager.LoginOrRegisterUser(mockRegistrationModel, mockPassword);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoginResult, Is.EqualTo(LoginResult.Success));
            Assert.That(result.ApplicationUser, Is.EqualTo(mockApplicationUser));
        }


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
    }
}