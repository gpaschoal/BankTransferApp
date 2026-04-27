using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Enums;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Transactions.DepositMoney;

public class DepositMoneyHandler(
        ILogger<DepositMoneyHandler> logger,
        IAccountRepository accountRepository,
        IDepositRepository depositRepository,
        IBalancePerMonthRepository balancePerMonthRepository,
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork
    ) : IHandler<DepositMoneyCommand, Result>
{
    public async Task<Result> HandleAsync(
        DepositMoneyCommand request,
        CancellationToken cancellationToken)
    {
        DepositMoneyValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return validationResult.ToResult();

        try
        {
            var accountExists = await accountRepository.ExistsAsync(request.AccountId, cancellationToken);

            if (!accountExists) return new("AccountNotFound", "The specified account does not exist.");

            var currentMonthReference = DateTime.UtcNow.Month;
            var currentYearReference = DateTime.UtcNow.Year;

            var balance = await balancePerMonthRepository.GetBalanceAsync(request.AccountId, currentMonthReference, currentYearReference, cancellationToken);

            if (balance is null)
            {
                balance = BalancePerMonthEntity.Create(request.AccountId, currentMonthReference, currentYearReference);
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                await balancePerMonthRepository.AddAsync(balance, cancellationToken);
                await unitOfWork.CommitTransactionAsync(cancellationToken);
            }

            await unitOfWork.BeginTransactionAsync(cancellationToken);
            var transaction = TransactionEntity.Create(request.Amount, ETransactionType.Deposit, request.AccountId, balance.Id);

            balance.AddTransaction(transaction);

            var deposit = DepositEntity.Create(request.Amount, request.AccountId, transaction.Id);

            await balancePerMonthRepository.UpdateAsync(balance, cancellationToken);

            await transactionRepository.AddAsync(transaction, cancellationToken);

            await depositRepository.AddAsync(deposit, cancellationToken);

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
