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
            var accountExists = await accountRepository.ExistsAsync(request.AccountId, cancellationToken);

            if (!accountExists) return new("AccountNotFound", "The specified account does not exist.");

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var transaction = TransactionEntity.Create(request.Amount, ETransactionType.Withdraw, request.AccountId);

            await transactionRepository.AddAsync(transaction, cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing DepositMoneyCommand.");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
