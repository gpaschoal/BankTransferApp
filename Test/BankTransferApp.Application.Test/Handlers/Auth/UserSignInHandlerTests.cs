using BankTransferApp.Application.Handlers.Auth.UserSignIn;
using BankTransferApp.Application.Shared.Commands;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BankTransferApp.Application.Test.Handlers.Auth;

[TestClass]
public class UserSignInHandlerTests
{
    private static UserSignInCommand MakeValidCommand(
        PersonNameCommand name = null,
        string cpf = null,
        AddressCommand address = null,
        TelephoneCommand cellphone = null,
        TelephoneCommand homePhone = null,
        string password = null,
        string passwordConfirmation = null)
    {
        name ??= new("John", "Doe");
        cpf ??= "12345678900";
        address ??= new("Av ", "West State", "dsd", "zip");
        cellphone ??= new("550", "555-1234");
        homePhone ??= new("550", "555-5678");
        password ??= "SecurePassword123";
        passwordConfirmation ??= "SecurePassword123";
        return new(name, cpf, address, cellphone, homePhone, password, passwordConfirmation);
    }

    [TestMethod]
    public async Task InvalidCommand_ShouldReturnValidationErrors()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();

        var sut = new UserSignInHandler(
                            logger: loggerMock.Object,
                            unitOfWork: unitOfWorkMock.Object,
                            userRepository: userRepositoryMock.Object,
                            passwordHasher: passwordHasherMock.Object);

        var command = new UserSignInCommand(null, null, null, null, null, null, null);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        result.IsValid.ShouldBeFalse();
        userRepositoryMock.Verify(r => r.UserExistsByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Never);
        unitOfWorkMock.Verify(u => u.BeginTransactionAsync(CancellationToken.None), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitTransactionAsync(CancellationToken.None), Times.Never);
        unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(CancellationToken.None), Times.Never);
        passwordHasherMock.Verify(p => p.Hash(It.IsAny<string>()), Times.Never);
        userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>(), CancellationToken.None), Times.Never);
    }

    [TestMethod]
    public async Task InvalidCommand_ShouldReturnCpfAlreadyExistsError()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();

        var command = MakeValidCommand();

        userRepositoryMock.Setup(r => r.UserExistsByCpfAsync(command.Cpf, CancellationToken.None)).ReturnsAsync(true);

        var sut = new UserSignInHandler(
                            logger: loggerMock.Object,
                            unitOfWork: unitOfWorkMock.Object,
                            userRepository: userRepositoryMock.Object,
                            passwordHasher: passwordHasherMock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem();
        result.Errors.ShouldContain(e => e.Value.Single().Equals("A user with the provided CPF already exists."));
        result.Errors.ShouldContain(e => e.Key.Equals(nameof(command.Cpf)));
        userRepositoryMock.Verify(r => r.UserExistsByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(u => u.BeginTransactionAsync(CancellationToken.None), Times.Never);
        unitOfWorkMock.Verify(u => u.CommitTransactionAsync(CancellationToken.None), Times.Never);
        unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(CancellationToken.None), Times.Never);
        passwordHasherMock.Verify(p => p.Hash(It.IsAny<string>()), Times.Never);
        userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>(), CancellationToken.None), Times.Never);
    }

    [TestMethod]
    public async Task ValidCommand_ShouldRegisterAnUser()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();

        var command = MakeValidCommand();

        userRepositoryMock.Setup(r => r.UserExistsByCpfAsync(command.Cpf, CancellationToken.None)).ReturnsAsync(false);
        passwordHasherMock.Setup(p => p.Hash(command.Password)).Returns("hashed-password");

        var sut = new UserSignInHandler(
                            logger: loggerMock.Object,
                            unitOfWork: unitOfWorkMock.Object,
                            userRepository: userRepositoryMock.Object,
                            passwordHasher: passwordHasherMock.Object);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();

        userRepositoryMock.Verify(r => r.UserExistsByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(u => u.BeginTransactionAsync(CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitTransactionAsync(CancellationToken.None), Times.Once);
        passwordHasherMock.Verify(p => p.Hash(command.Password), Times.Once);
        userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>(), CancellationToken.None), Times.Once);

        unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(CancellationToken.None), Times.Never);
    }

    [TestMethod]
    public async Task InvalidCommand_ShouldRollbackWhenThrowsAnException()
    {
        var loggerMock = new Mock<ILogger<UserSignInHandler>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher>();

        var command = MakeValidCommand();

        userRepositoryMock.Setup(r => r.UserExistsByCpfAsync(command.Cpf, CancellationToken.None)).ReturnsAsync(false);
        passwordHasherMock.Setup(p => p.Hash(command.Password)).Returns("hashed-password");

        unitOfWorkMock.Setup(u => u.CommitTransactionAsync(CancellationToken.None)).ThrowsAsync(new Exception("Oh no! Commit failed!"));

        var sut = new UserSignInHandler(
                            logger: loggerMock.Object,
                            unitOfWork: unitOfWorkMock.Object,
                            userRepository: userRepositoryMock.Object,
                            passwordHasher: passwordHasherMock.Object);

        try
        {
            _ = await sut.HandleAsync(command, CancellationToken.None);
        }
        catch { /* It throwns an error that should not be thrown!!! */ }

        userRepositoryMock.Verify(r => r.UserExistsByCpfAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(u => u.BeginTransactionAsync(CancellationToken.None), Times.Once);
        unitOfWorkMock.Verify(u => u.CommitTransactionAsync(CancellationToken.None), Times.Once);
        passwordHasherMock.Verify(p => p.Hash(command.Password), Times.Once);
        userRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<UserEntity>(), CancellationToken.None), Times.Once);

        unitOfWorkMock.Verify(u => u.RollbackTransactionAsync(CancellationToken.None), Times.Once);
    }
}