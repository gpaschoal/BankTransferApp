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

    public static TransactionEntity Create(
        decimal value,
        ETransactionType type,
        Guid accountId,
        Guid balanceId)
    {
        return new TransactionEntity
        {
            Id = Guid.CreateVersion7(),
            Value = value,
            Reference = DateTime.UtcNow,
            Type = type,
            AccountId = accountId,
            BalanceId = balanceId
        };
    }
}
