using BankTransferApp.Application.Handlers.Auth.UserSignIn;
using BankTransferApp.Application.Shared.Options;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using BankTransferApp.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;

namespace BankTransferApp.Application.Test.Handlers.Auth;

[TestClass]
public class UserSignInHandlerTests
{
    private static UserSignInCommand MakeValidCommand(
        string cpf = null,
        string password = null)
    {
        cpf ??= "12345678900";
        password ??= "SecurePassword123";
        return new(cpf, password);
    }

    [TestMethod(DisplayName = "Should Return Validation Errors")]
    public async Task InvalidCommand_ShouldReturnValidationErrors()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var tokenOptionMock = new Mock<IOptions<TokenOption>>();

        var sut = new UserSignInHandler(
                        loggerMock.Object,
                        userRepositoryMock.Object,
                        passwordHasherMock.Object,
                        tokenServiceMock.Object,
                        tokenOptionMock.Object);
        var command = new UserSignInCommand(null, null);
        var result = await sut.HandleAsync(command, CancellationToken.None);
        result.IsValid.ShouldBeFalse();
        result.Data.Token.ShouldBeNullOrWhiteSpace();
        result.Data.ExpireAt.ShouldBeNull();
        userRepositoryMock.Verify(x => x.GetUserByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Never);
        passwordHasherMock.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<UserEntity>(), It.IsAny<Dictionary<string, string>>()), Times.Never);
    }

    [TestMethod(DisplayName = "Should Return Invalid When Does Not Find User By CPF")]
    public async Task InvalidCommand_ShouldReturnInvalidWhenDoesNotFindUserByCPF()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var tokenOptionMock = new Mock<IOptions<TokenOption>>();

        userRepositoryMock.Setup(x => x.GetUserByCpfAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync((UserEntity)null);

        var sut = new UserSignInHandler(
                        loggerMock.Object,
                        userRepositoryMock.Object,
                        passwordHasherMock.Object,
                        tokenServiceMock.Object,
                        tokenOptionMock.Object);
        var command = MakeValidCommand();
        var result = await sut.HandleAsync(command, CancellationToken.None);
        result.IsValid.ShouldBeFalse();
        result.Errors.Single().Value.Single().ShouldBe("Invalid CPF or password.");
        result.Errors.Single().Key.ShouldBe("Authentication");
        result.Data.ShouldBeNull();
        userRepositoryMock.Verify(x => x.GetUserByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        passwordHasherMock.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<UserEntity>(), It.IsAny<Dictionary<string, string>>()), Times.Never);
    }

    [TestMethod(DisplayName = "Should Return Invalid When Password Does Not Match")]
    public async Task InvalidCommand_ShouldReturnInvalidWhenPasswordDoesNotMatch()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var tokenOptionMock = new Mock<IOptions<TokenOption>>();
        var userEntity = UserEntity.Create(
            new PersonNameValueObject("John", "Doe"),
            new CpfValueObject("12345678900"),
            new AddressValueObject("123 Main St", "City", "State", "12345"),
            new TelephoneValueObject("22", "1234567890"),
            new TelephoneValueObject("22", "1234567890"),
            new PasswordValueObject("hashedPassword"));

        userRepositoryMock.Setup(x => x.GetUserByCpfAsync(userEntity.CpfDocument.Value, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(userEntity);
        passwordHasherMock.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(false);

        var sut = new UserSignInHandler(
                        loggerMock.Object,
                        userRepositoryMock.Object,
                        passwordHasherMock.Object,
                        tokenServiceMock.Object,
                        tokenOptionMock.Object);
        var command = MakeValidCommand();
        var result = await sut.HandleAsync(command, CancellationToken.None);
        result.IsValid.ShouldBeFalse();
        result.Errors.Single().Value.Single().ShouldBe("Invalid CPF or password.");
        result.Errors.Single().Key.ShouldBe("Authentication");
        result.Data.ShouldBeNull();
        userRepositoryMock.Verify(x => x.GetUserByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        passwordHasherMock.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<UserEntity>(), It.IsAny<Dictionary<string, string>>()), Times.Never);
    }

    [TestMethod(DisplayName = "Should Return Token")]
    public async Task ValidCommand_ShouldReturnToken()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();
        var tokenServiceMock = new Mock<ITokenService>();
        var tokenOptionMock = new Mock<IOptions<TokenOption>>();
        var userEntity = UserEntity.Create(
            new PersonNameValueObject("John", "Doe"),
            new CpfValueObject("12345678900"),
            new AddressValueObject("123 Main St", "City", "State", "12345"),
            new TelephoneValueObject("22", "1234567890"),
            new TelephoneValueObject("22", "1234567890"),
            new PasswordValueObject("hashedPassword"));

        userRepositoryMock.Setup(x => x.GetUserByCpfAsync(userEntity.CpfDocument.Value, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(userEntity);
        passwordHasherMock.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
                          .Returns(true);
        tokenServiceMock.Setup(x => x.GenerateToken(userEntity, It.IsAny<Dictionary<string, string>>()))
                        .Returns("mockedToken");
        tokenOptionMock.Setup(x => x.Value)
                       .Returns(new TokenOption { ExpirationInHours = 1 });
        var sut = new UserSignInHandler(
                        loggerMock.Object,
                        userRepositoryMock.Object,
                        passwordHasherMock.Object,
                        tokenServiceMock.Object,
                        tokenOptionMock.Object);
        var command = MakeValidCommand();
        var startDate = DateTime.UtcNow;
        var result = await sut.HandleAsync(command, CancellationToken.None);
        var endDate = DateTime.UtcNow.AddHours(tokenOptionMock.Object.Value.ExpirationInHours);
        result.IsValid.ShouldBeTrue();
        result.Data.Token.ShouldBe("mockedToken");
        result.Data.ExpireAt.Value.ShouldBeGreaterThan(startDate);
        result.Data.ExpireAt.Value.ShouldBeLessThan(endDate);
        userRepositoryMock.Verify(x => x.GetUserByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        passwordHasherMock.Verify(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<UserEntity>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
    }
}
