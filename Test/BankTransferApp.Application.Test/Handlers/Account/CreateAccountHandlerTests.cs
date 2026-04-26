using BankTransferApp.Application.Handlers.Account.CreateAccount;
using BankTransferApp.Application.Service;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Enums;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace BankTransferApp.Application.Test.Handlers.Account;

[TestClass]
public class CreateAccountHandlerTests
{
    [TestMethod(DisplayName = "Should Return Validation Errors")]
    public async Task InvalidCommand_ShouldReturnValidationErrors()
    {

        var loggerMock = new Mock<ILogger<CreateAccountHandler>>();
        var accountRepositoryMock = new Mock<IAccountRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userContextServiceMock = new Mock<IUserContextService>();

        var sut = new CreateAccountHandler(
            loggerMock.Object,
            accountRepositoryMock.Object,
            userRepositoryMock.Object,
            unitOfWorkMock.Object,
            userContextServiceMock.Object);

        CreateAccountCommand command = new((EAccountType)999);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldNotBeEmpty();

        userContextServiceMock.Verify(x => x.ThrownsIfUserNotLoggedIn(), Times.Never);
        userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod(DisplayName = "Should Thrown An Exception If User Is Not Logged In")]
    public async Task InvalidCommand_ShouldThrownAnExceptionIfUserIsNotLoggedIn()
    {
        var loggerMock = new Mock<ILogger<CreateAccountHandler>>();
        var accountRepositoryMock = new Mock<IAccountRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userContextService = new UserContextService();

        var sut = new CreateAccountHandler(
            loggerMock.Object,
            accountRepositoryMock.Object,
            userRepositoryMock.Object,
            unitOfWorkMock.Object,
            userContextService);

        CreateAccountCommand command = new(EAccountType.CurrentAccount);

        await sut.HandleAsync(command, CancellationToken.None).ShouldThrowAsync<InvalidOperationException>();

        userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod(DisplayName = "Should Return An Invalid Result If User Not Found")]
    public async Task InvalidCommand_ShouldReturnAnInvalidResultIfUserNotFound()
    {
        var loggerMock = new Mock<ILogger<CreateAccountHandler>>();
        var accountRepositoryMock = new Mock<IAccountRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userContextService = new UserContextService();
        userContextService.SetCurrentUserId(Guid.NewGuid());

        var sut = new CreateAccountHandler(
            loggerMock.Object,
            accountRepositoryMock.Object,
            userRepositoryMock.Object,
            unitOfWorkMock.Object,
            userContextService);

        CreateAccountCommand command = new(EAccountType.CurrentAccount);

        userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserEntity)null);

        var result = await sut.HandleAsync(command, CancellationToken.None);

        result.Errors.Single().Key.ShouldBe("InvalidUser");
        result.Errors.Single().Value.Single().ShouldBe("User not found!");

        userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod(DisplayName = "Should Return An Invalid Result If User Is Not Active")]
    public async Task InvalidCommand_ShouldReturnAnInvalidResultIfUserIsNotActive()
    {
        var loggerMock = new Mock<ILogger<CreateAccountHandler>>();
        var accountRepositoryMock = new Mock<IAccountRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var userContextService = new UserContextService();
        userContextService.SetCurrentUserId(Guid.NewGuid());

        var sut = new CreateAccountHandler(
            loggerMock.Object,
            accountRepositoryMock.Object,
            userRepositoryMock.Object,
            unitOfWorkMock.Object,
            userContextService);

        CreateAccountCommand command = new(EAccountType.CurrentAccount);

        userRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserEntity { IsActive = false });

        var result = await sut.HandleAsync(command, CancellationToken.None);

        result.Errors.Single().Key.ShouldBe("InvalidUser");
        result.Errors.Single().Value.Single().ShouldBe("User must be active to create an account!");

        userRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        accountRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AccountEntity>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.CommitTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
        unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
