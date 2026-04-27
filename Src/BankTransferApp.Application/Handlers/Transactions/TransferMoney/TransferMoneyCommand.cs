namespace BankTransferApp.Application.Handlers.Transactions.TransferMoney;

public record TransferMoneyCommand(Guid SourceAccountId, Guid DestinationAccountId, decimal Amount);
