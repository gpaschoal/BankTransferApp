using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Account.DeactivateAccount;

public class DeactivateAccountHandler(
        ILogger<DeactivateAccountHandler> logger,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        IUserContextService userContextService
    ) : IHandler<DeactivateAccountCommand, Result>
{
    public async Task<Result> HandleAsync(
        DeactivateAccountCommand request,
        CancellationToken cancellationToken)
    {
        DeactivateAccountCommandValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return validationResult.ToResult();

        userContextService.ThrownsIfUserNotLoggedIn();

        try
        {
            var userId = userContextService.CurrentUserId.Value;

            var account = await accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

            if (account is null) return new("InvalidAccount", "Account not found.");

            if (account.OwnerId != userId) return new("Unauthorized", "You are not authorized to deactivate this account.");

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            account.Deactivate();

            account.SetModifiedBy(userId);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while deactivating the account with ID {AccountId}", request.AccountId);
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
