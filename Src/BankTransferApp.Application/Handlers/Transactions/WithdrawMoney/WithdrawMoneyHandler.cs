using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Enums;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Transactions.WithdrawMoney;

public class WithdrawMoneyHandler(
        ILogger<WithdrawMoneyHandler> logger,
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        IUserContextService userContextService
    ) : IHandler<WithdrawMoneyCommand, Result>
{
    public async Task<Result> HandleAsync(
        WithdrawMoneyCommand request,
        CancellationToken cancellationToken)
    {
        WithdrawMoneyValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return validationResult.ToResult();

        userContextService.ThrownsIfUserNotLoggedIn();

        try
        {
            var account = await accountRepository.GetByIdAsync(request.AccountId, cancellationToken);

            if (account is null) return new("AccountNotFound", "The specified account does not exist.");

            if (account.OwnerId != userContextService.CurrentUserId.Value)
                return new("Unauthorized", "You do not have permission to perform this action on the specified account.");

            if (!account.IsActive) return new("AccountInactive", "The specified account is inactive.");

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var transaction = TransactionEntity.Create(request.Amount, ETransactionType.Withdraw, request.AccountId);

            await transactionRepository.AddAsync(transaction, cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing WithdrawMoneyCommand.");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
