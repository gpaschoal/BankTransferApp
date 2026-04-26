using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Account.CreateAccount;

public class CreateAccountHandler(
        ILogger<CreateAccountHandler> logger,
        IAccountRepository accountRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IUserContextService userContextService
    ) : IHandler<CreateAccountCommand, Result<Guid>>
{
    public async Task<Result<Guid>> HandleAsync(
        CreateAccountCommand request,
        CancellationToken cancellationToken)
    {
        CreateAccountValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid) return validationResult.ToResult<Guid>();

        userContextService.TrownsIfUserNotLoggedIn();

        try
        {
            var userId = userContextService.CurrentUserId.Value;

            var user = await userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null) return new("InvalidUser", "User not found!");

            if (!user.IsActive) return new("InvalidUser", "User must be active to create an account!");

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var account = AccountEntity.Create(userId, request.AccountType, userId);

            await accountRepository.AddAsync(account, cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new(account.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing CreateAccountCommand.");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
