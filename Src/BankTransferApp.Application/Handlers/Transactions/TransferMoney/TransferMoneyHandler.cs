using BankTransferApp.Application.Shared;
using BankTransferApp.Domain.Entities;
using BankTransferApp.Domain.Enums;
using BankTransferApp.Domain.Handlers;
using BankTransferApp.Domain.Repositories;
using BankTransferApp.Domain.Services;
using Microsoft.Extensions.Logging;

namespace BankTransferApp.Application.Handlers.Transactions.TransferMoney;

public class TransferMoneyHandler(
        ILogger<TransferMoneyHandler> logger,
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository,
        ITransferRepository transferRepository,
        IUnitOfWork unitOfWork,
        IUserContextService userContextService
    ) : IHandler<TransferMoneyCommand, Result>
{
    public async Task<Result> HandleAsync(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        TransferMoneyValidator validator = new();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) return validationResult.ToResult();

        userContextService.ThrownsIfUserNotLoggedIn();

        try
        {
            var sourceAccount = await accountRepository.GetByIdAsync(request.SourceAccountId, cancellationToken);

            if (sourceAccount is null) return new("AccountNotFound", "The specified source account does not exist.");

            if (sourceAccount.OwnerId != userContextService.CurrentUserId.Value)
                return new("Unauthorized", "You do not have permission to perform this action on the specified source account.");

            var destinationAccountExists = await accountRepository.ExistsAsync(request.DestinationAccountId, cancellationToken);

            if (!destinationAccountExists) return new("AccountNotFound", "The specified destination account does not exist.");

            var transactionSource = TransactionEntity.Create(request.Amount * -1, ETransactionType.TransferOut, sourceAccount.Id);

            var transactionDestiny = TransactionEntity.Create(request.Amount, ETransactionType.TransferIn, request.DestinationAccountId);

            var transfer = TransferEntity.Create(request.Amount, request.SourceAccountId, request.DestinationAccountId);

            await unitOfWork.BeginTransactionAsync(cancellationToken);

            await transactionRepository.AddAsync(transactionSource, cancellationToken);

            await transactionRepository.AddAsync(transactionDestiny, cancellationToken);

            await transferRepository.AddAsync(transfer, cancellationToken);

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return new();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing TransferMoneyCommand.");
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
