namespace BankTransferApp.Application.Handlers.Transactions.WithdrawMoney;

public record WithdrawMoneyCommand(Guid AccountId, decimal Amount);
