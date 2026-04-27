using BankTransferApp.Domain.Enums;

namespace BankTransferApp.Domain.Entities;

public class TransactionEntity : IEntity
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public DateTime Reference { get; set; }
    public ETransactionType Type { get; set; }
    public Guid AccountId { get; set; }
    public AccountEntity Account { get; set; }
    public Guid BalanceId { get; set; }
    public BalancePerMonthEntity Balance { get; set; }
}
