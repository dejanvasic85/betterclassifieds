using System;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Broadcast;
using Paramount.Betterclassifieds.Business.Payment;
using Paramount.Betterclassifieds.Tests.Mocks;

namespace Paramount.Betterclassifieds.Tests.Membership
{
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
        public void ConfirmRegistration_RegistrationExpired()
        {
            // Arrange
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithRegistrationId(800)
                .WithToken("t202")
                .WithUsername("user123")
                .WithExpirationDateUtc(DateTime.Now)
                .Build();


            _mockAuthManager.SetupWithVerification(call => call.GetRegistration(It.IsAny<int>()), mockRegistrationModel);
            _mockDateService.SetupWithVerification(call => call.UtcNow, DateTime.Now.AddHours(1)); // ahead of the registration model

            var confirmResult = BuildTargetObject().ConfirmRegistration(100, string.Empty);

            confirmResult.IsEqualTo(RegistrationConfirmationResult.RegistrationExpired);
        }

        [Test]
        public void ConfirmRegistration_TokenExpired()
        {
            // Arrange
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithRegistrationId(800)
                .WithToken("t202")
                .WithUsername("user123")
                .WithConfirmationAttempts(6)
                .WithExpirationDateUtc(DateTime.Now.AddDays(1))
                .Build();


            _mockAuthManager.SetupWithVerification(call => call.GetRegistration(It.IsAny<int>()), mockRegistrationModel);
            _mockDateService.SetupWithVerification(call => call.UtcNow, DateTime.Now.AddHours(1)); // ahead of the registration model

            var confirmResult = BuildTargetObject().ConfirmRegistration(100, string.Empty);

            confirmResult.IsEqualTo(RegistrationConfirmationResult.TokenExpired);
        }

        [Test]
        public void ConfirmRegistration_TokenNotCorrect()
        {
            // Arrange
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithRegistrationId(800)
                .WithToken("t202")
                .WithUsername("user123")
                .WithConfirmationAttempts(1)
                .WithExpirationDateUtc(DateTime.Now.AddDays(1))
                .Build();

            
            _mockAuthManager.SetupWithVerification(call => call.GetRegistration(It.IsAny<int>()), mockRegistrationModel);
            _mockDateService.SetupWithVerification(call => call.UtcNow, DateTime.Now.AddHours(1)); // ahead of the registration model
            _mockUserRepository.SetupWithVerification(call => call.UpdateRegistrationByToken(It.IsAny<RegistrationModel>()));

            // act
            var confirmResult = BuildTargetObject().ConfirmRegistration(100, token: "fail123");

            // assert
            confirmResult.IsEqualTo(RegistrationConfirmationResult.TokenNotCorrect);
        }

        [Test]
        public void ConfirmRegistration_Successful()
        {
            // Arrange
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithRegistrationId(800)
                .WithToken("t202")
                .WithUsername("user123")
                .WithConfirmationAttempts(1)
                .WithExpirationDateUtc(DateTime.Now.AddHours(2))
                .WithUsername("user123")
                .WithEncryptedPassword("CftfT8Mg7C4aMzwXNZKnTA==")
                .WithEmail("email@123.com")
                .Build();


            _mockAuthManager.SetupWithVerification(call => call.GetRegistration(It.IsAny<int>()), mockRegistrationModel);
            _mockAuthManager.SetupWithVerification(call => call.CreateMembership(mockRegistrationModel.Username,
                mockRegistrationModel.Email, 
                mockRegistrationModel.DecryptPassword(),
                true));
            _mockDateService.SetupWithVerification(call => call.UtcNow, DateTime.Now.AddHours(1)); // ahead of the registration model
            _mockUserRepository.SetupWithVerification(call => call.CreateUserProfile(mockRegistrationModel));
            _mockUserRepository.SetupWithVerification(call => call.UpdateRegistrationByToken(mockRegistrationModel));


            var confirmResult = BuildTargetObject().ConfirmRegistration(100, "t202");

            confirmResult.IsEqualTo(RegistrationConfirmationResult.Successful);
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
        public void LoginOrRegister_UserExists_ValidatesPassword_ReturnsSuccess()
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
            _mockAuthManager.SetupWithVerification(call => call.Login(It.Is<string>(str => str == mockApplicationUser.Username), It.Is<bool>(p => p == false), It.IsAny<string>()));

            // act
            var userManager = BuildTargetObject();
            var result = userManager.LoginOrRegister(mockRegistrationModel, mockPassword);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoginResult, Is.EqualTo(LoginResult.Success));
            Assert.That(result.ApplicationUser, Is.EqualTo(mockApplicationUser));
        }

        [Test]
        public void LoginOrRegister_UserExists_BadPassword()
        {
            // arrange data
            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithEmail(mockApplicationUser.Email)
                .Build();
            var mockPassword = "password321";

            // arrange service calls
            _mockUserRepository.SetupWithVerification(call => call.GetUserByEmail(It.Is<string>(str => str == mockApplicationUser.Email)), mockApplicationUser);
            _mockAuthManager.SetupWithVerification(call => call.ValidatePassword(It.Is<string>(str => str == mockApplicationUser.Username), It.Is<string>(str => str == mockPassword)), false);

            // act
            var userManager = BuildTargetObject();
            var result = userManager.LoginOrRegister(mockRegistrationModel, mockPassword);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoginResult, Is.EqualTo(LoginResult.BadUsernameOrPassword));
            Assert.That(result.ApplicationUser, Is.Null);
        }

        [Test]
        public void LoginOrRegister_UserDoesNotExists_IsRegistered_AndLoggedIn()
        {
            // Arrange data
            var mockApplicationUser = new ApplicationUserMockBuilder().Default().Build();
        
            var mockRegistrationModel = new RegistrationModelMockBuilder()
                .WithRegistrationId(800)
                .WithToken("t202")
                .WithUsername("user123")
                .WithConfirmationAttempts(1)
                .WithExpirationDateUtc(DateTime.UtcNow.AddMinutes(10))
                .WithUsername("user123")
                .WithEncryptedPassword("CftfT8Mg7C4aMzwXNZKnTA==")
                .WithEmail(mockApplicationUser.Email)
                .Build();

            var mockPassword = "password321";

            // Arrange service calls
            _mockUserRepository.SetupSequence(call => call.GetUserByEmail(It.Is<string>(str => str == mockApplicationUser.Email)))
                .Returns(null) // First time there is no user
                .Returns(mockApplicationUser); // 2nd time we get the user :)
            _mockUserRepository.SetupWithVerification(call => call.CreateRegistration(It.Is<RegistrationModel>(r => r == mockRegistrationModel)));
            _mockUserRepository.SetupWithVerification(call => call.CreateUserProfile(mockRegistrationModel));
            _mockUserRepository.SetupWithVerification(call => call.UpdateRegistrationByToken(mockRegistrationModel));
            
            _mockAuthManager.SetupWithVerification(call => call.Login(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<string>()));
            _mockAuthManager.SetupWithVerification(call => call.CheckUsernameExists(It.IsAny<string>()), false);
            _mockAuthManager.SetupWithVerification(call => call.GetRegistration(mockRegistrationModel.RegistrationId.Value), mockRegistrationModel);
            _mockAuthManager.SetupWithVerification(call => call.CreateMembership(It.IsAny<string>(),
                 It.IsAny<string>(),
                 It.IsAny<string>(),
                 It.IsAny<bool>()));

            _mockConfig.SetupWithVerification(call => call.EnableRegistrationEmailVerification, true);
            _mockCodeGenerator.SetupWithVerification(call => call.GenerateCode(), new ConfirmationCodeResult("1234", DateTime.Now.AddMinutes(10), DateTime.UtcNow.AddMinutes(10)));
            _mockDateService.SetupWithVerification(call => call.UtcNow, DateTime.UtcNow); // ahead of the registration model

            // Act
            var userManager = BuildTargetObject();
            var result = userManager.LoginOrRegister(mockRegistrationModel, mockPassword);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.LoginResult, Is.EqualTo(LoginResult.Success));
            Assert.That(result.ApplicationUser, Is.Not.Null);
        }

        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IAuthManager> _mockAuthManager;
        private Mock<IClientConfig> _mockConfig;
        private Mock<IConfirmationCodeGenerator> _mockCodeGenerator;
        private Mock<IDateService> _mockDateService;

        [SetUp]
        public void SetupMocks()
        {
            _mockUserRepository = CreateMockOf<IUserRepository>();
            _mockAuthManager = CreateMockOf<IAuthManager>();
            _mockConfig = CreateMockOf<IClientConfig>();
            _mockCodeGenerator = CreateMockOf<IConfirmationCodeGenerator>();
            _mockDateService = CreateMockOf<IDateService>();
        }
    }
}