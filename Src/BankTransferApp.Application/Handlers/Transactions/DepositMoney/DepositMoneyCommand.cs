namespace BankTransferApp.Application.Handlers.Transactions.DepositMoney;

public record DepositMoneyCommand(Guid AccountId, decimal Amount);
